using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Utility.ServerConnection
{
    partial class FrmMultiServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMultiServer));
            this.lbl_DatabaseServer = new System.Windows.Forms.Label();
            this.lbl_Login = new System.Windows.Forms.Label();
            this.TxtServerInfo = new MrTextBox();
            this.lbl_Password = new System.Windows.Forms.Label();
            this.TxtPassword = new MrTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtSecondServerPassword = new MrTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtSecondServerName = new MrTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TxtSecondServerUser = new MrTextBox();
            this.cb_ServerName = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtUser = new MrTextBox();
            this.ChkMultiServer = new System.Windows.Forms.CheckBox();
            this.GrpButton = new System.Windows.Forms.GroupBox();
            this.BtnConnect = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BtnOnlineServer = new DevExpress.XtraEditors.SimpleButton();
            this.ChkIsOnlineSync = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.GrpButton.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_DatabaseServer
            // 
            this.lbl_DatabaseServer.AutoSize = true;
            this.lbl_DatabaseServer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_DatabaseServer.Location = new System.Drawing.Point(6, 27);
            this.lbl_DatabaseServer.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_DatabaseServer.Name = "lbl_DatabaseServer";
            this.lbl_DatabaseServer.Size = new System.Drawing.Size(87, 20);
            this.lbl_DatabaseServer.TabIndex = 0;
            this.lbl_DatabaseServer.Text = "Serve Info";
            // 
            // lbl_Login
            // 
            this.lbl_Login.AutoSize = true;
            this.lbl_Login.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_Login.Location = new System.Drawing.Point(7, 57);
            this.lbl_Login.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_Login.Name = "lbl_Login";
            this.lbl_Login.Size = new System.Drawing.Size(100, 20);
            this.lbl_Login.TabIndex = 1;
            this.lbl_Login.Text = "Server User";
            // 
            // TxtServerInfo
            // 
            this.TxtServerInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtServerInfo.Location = new System.Drawing.Point(109, 24);
            this.TxtServerInfo.Margin = new System.Windows.Forms.Padding(5);
            this.TxtServerInfo.Name = "TxtServerInfo";
            this.TxtServerInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtServerInfo.Size = new System.Drawing.Size(226, 26);
            this.TxtServerInfo.TabIndex = 0;
            this.TxtServerInfo.Leave += new System.EventHandler(this.TxtServerInfo_Leave);
            // 
            // lbl_Password
            // 
            this.lbl_Password.AutoSize = true;
            this.lbl_Password.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_Password.Location = new System.Drawing.Point(6, 87);
            this.lbl_Password.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_Password.Name = "lbl_Password";
            this.lbl_Password.Size = new System.Drawing.Size(82, 20);
            this.lbl_Password.TabIndex = 4;
            this.lbl_Password.Text = "Password";
            // 
            // TxtPassword
            // 
            this.TxtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPassword.Location = new System.Drawing.Point(109, 84);
            this.TxtPassword.Margin = new System.Windows.Forms.Padding(5);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.PasswordChar = '*';
            this.TxtPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtPassword.Size = new System.Drawing.Size(226, 26);
            this.TxtPassword.TabIndex = 2;
            this.TxtPassword.Leave += new System.EventHandler(this.TxtPassword_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(7, 26);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Server Info";
            // 
            // TxtSecondServerPassword
            // 
            this.TxtSecondServerPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSecondServerPassword.Location = new System.Drawing.Point(111, 82);
            this.TxtSecondServerPassword.Margin = new System.Windows.Forms.Padding(5);
            this.TxtSecondServerPassword.Name = "TxtSecondServerPassword";
            this.TxtSecondServerPassword.PasswordChar = '*';
            this.TxtSecondServerPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtSecondServerPassword.Size = new System.Drawing.Size(225, 26);
            this.TxtSecondServerPassword.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(7, 55);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Server User";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(7, 85);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "Password";
            // 
            // TxtSecondServerName
            // 
            this.TxtSecondServerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSecondServerName.Location = new System.Drawing.Point(111, 23);
            this.TxtSecondServerName.Margin = new System.Windows.Forms.Padding(5);
            this.TxtSecondServerName.Name = "TxtSecondServerName";
            this.TxtSecondServerName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtSecondServerName.Size = new System.Drawing.Size(225, 26);
            this.TxtSecondServerName.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TxtSecondServerUser);
            this.groupBox2.Controls.Add(this.TxtSecondServerName);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.TxtSecondServerPassword);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cb_ServerName);
            this.groupBox2.Location = new System.Drawing.Point(349, 206);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox2.Size = new System.Drawing.Size(342, 114);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Secondary GetServer";
            // 
            // TxtSecondServerUser
            // 
            this.TxtSecondServerUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSecondServerUser.Location = new System.Drawing.Point(111, 53);
            this.TxtSecondServerUser.Margin = new System.Windows.Forms.Padding(5);
            this.TxtSecondServerUser.Name = "TxtSecondServerUser";
            this.TxtSecondServerUser.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtSecondServerUser.Size = new System.Drawing.Size(225, 26);
            this.TxtSecondServerUser.TabIndex = 1;
            // 
            // cb_ServerName
            // 
            this.cb_ServerName.FormattingEnabled = true;
            this.cb_ServerName.Location = new System.Drawing.Point(168, 82);
            this.cb_ServerName.Margin = new System.Windows.Forms.Padding(5);
            this.cb_ServerName.Name = "cb_ServerName";
            this.cb_ServerName.Size = new System.Drawing.Size(126, 28);
            this.cb_ServerName.TabIndex = 7;
            this.cb_ServerName.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtUser);
            this.groupBox1.Controls.Add(this.lbl_DatabaseServer);
            this.groupBox1.Controls.Add(this.TxtPassword);
            this.groupBox1.Controls.Add(this.TxtServerInfo);
            this.groupBox1.Controls.Add(this.lbl_Password);
            this.groupBox1.Controls.Add(this.lbl_Login);
            this.groupBox1.Location = new System.Drawing.Point(0, 206);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(343, 114);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Primary Server";
            // 
            // TxtUser
            // 
            this.TxtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUser.Location = new System.Drawing.Point(109, 54);
            this.TxtUser.Margin = new System.Windows.Forms.Padding(5);
            this.TxtUser.Name = "TxtUser";
            this.TxtUser.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtUser.Size = new System.Drawing.Size(226, 26);
            this.TxtUser.TabIndex = 1;
            this.TxtUser.Leave += new System.EventHandler(this.TxtUser_Leave);
            // 
            // ChkMultiServer
            // 
            this.ChkMultiServer.Checked = true;
            this.ChkMultiServer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkMultiServer.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.ChkMultiServer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ChkMultiServer.Location = new System.Drawing.Point(3, 14);
            this.ChkMultiServer.Margin = new System.Windows.Forms.Padding(5);
            this.ChkMultiServer.Name = "ChkMultiServer";
            this.ChkMultiServer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkMultiServer.Size = new System.Drawing.Size(125, 28);
            this.ChkMultiServer.TabIndex = 2;
            this.ChkMultiServer.Text = "Multi Server";
            this.ChkMultiServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkMultiServer.UseVisualStyleBackColor = true;
            // 
            // GrpButton
            // 
            this.GrpButton.Controls.Add(this.BtnConnect);
            this.GrpButton.Controls.Add(this.BtnExit);
            this.GrpButton.Controls.Add(this.ChkMultiServer);
            this.GrpButton.Location = new System.Drawing.Point(0, 312);
            this.GrpButton.Name = "GrpButton";
            this.GrpButton.Size = new System.Drawing.Size(344, 48);
            this.GrpButton.TabIndex = 1;
            this.GrpButton.TabStop = false;
            // 
            // BtnConnect
            // 
            this.BtnConnect.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnConnect.Appearance.Options.UseFont = true;
            this.BtnConnect.Location = new System.Drawing.Point(133, 11);
            this.BtnConnect.Margin = new System.Windows.Forms.Padding(5);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(123, 34);
            this.BtnConnect.TabIndex = 0;
            this.BtnConnect.Text = "C&ONNECT";
            this.BtnConnect.Click += new System.EventHandler(this.BtnSaveServer_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnExit.Location = new System.Drawing.Point(258, 11);
            this.BtnExit.Margin = new System.Windows.Forms.Padding(5);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(85, 34);
            this.BtnExit.TabIndex = 1;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BtnOnlineServer);
            this.groupBox3.Controls.Add(this.ChkIsOnlineSync);
            this.groupBox3.Location = new System.Drawing.Point(348, 311);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(343, 49);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            // 
            // BtnOnlineServer
            // 
            this.BtnOnlineServer.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnOnlineServer.Appearance.Options.UseFont = true;
            this.BtnOnlineServer.Location = new System.Drawing.Point(202, 13);
            this.BtnOnlineServer.Margin = new System.Windows.Forms.Padding(5);
            this.BtnOnlineServer.Name = "BtnOnlineServer";
            this.BtnOnlineServer.Size = new System.Drawing.Size(134, 34);
            this.BtnOnlineServer.TabIndex = 0;
            this.BtnOnlineServer.Text = "C&ONNECT";
            this.BtnOnlineServer.Click += new System.EventHandler(this.BtnOnlineServer_Click);
            // 
            // ChkIsOnlineSync
            // 
            this.ChkIsOnlineSync.Checked = true;
            this.ChkIsOnlineSync.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkIsOnlineSync.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.ChkIsOnlineSync.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ChkIsOnlineSync.Location = new System.Drawing.Point(3, 18);
            this.ChkIsOnlineSync.Margin = new System.Windows.Forms.Padding(5);
            this.ChkIsOnlineSync.Name = "ChkIsOnlineSync";
            this.ChkIsOnlineSync.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIsOnlineSync.Size = new System.Drawing.Size(189, 28);
            this.ChkIsOnlineSync.TabIndex = 3;
            this.ChkIsOnlineSync.Text = "IsOnline Sync";
            this.ChkIsOnlineSync.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIsOnlineSync.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::MrBLL.Properties.Resources.sqlserver;
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(345, 208);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // FrmMultiServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(345, 360);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GrpButton);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMultiServer";
            this.Text = "Server Configuration";
            this.Load += new System.EventHandler(this.FrmMultiServer_Load);
            this.Shown += new System.EventHandler(this.FrmMultiServer_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmMultiServer_KeyPress);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.GrpButton.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Label lbl_Login;
        private System.Windows.Forms.Label lbl_DatabaseServer;
        private System.Windows.Forms.Label lbl_Password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox ChkMultiServer;
        private DevExpress.XtraEditors.SimpleButton BtnConnect;
        private System.Windows.Forms.ComboBox cb_ServerName;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private System.Windows.Forms.GroupBox GrpButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraEditors.SimpleButton BtnOnlineServer;
        private System.Windows.Forms.CheckBox ChkIsOnlineSync;
        private MrTextBox TxtServerInfo;
        private MrTextBox TxtPassword;
        private MrTextBox TxtSecondServerPassword;
        private MrTextBox TxtSecondServerName;
        private MrTextBox TxtUser;
        private MrTextBox TxtSecondServerUser;
    }
}