using MrDAL.Control.ControlsEx.Control;
using MrDAL.Global.Common;

namespace MrBLL.Setup.UserSetup
{
    partial class FrmLogin
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
        [System.Obsolete]
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.lnkReset = new System.Windows.Forms.LinkLabel();
            this.clsSeparator3 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.lbl_Login = new System.Windows.Forms.Label();
            this.TxtUserName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtUserPassword = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.mrPanel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.mrGroup2 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.mrGroup1 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.LblSoftwareSpec = new System.Windows.Forms.Label();
            this.LblSoftwareCaption = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.mrPanel1.SuspendLayout();
            this.mrGroup2.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lnkReset
            // 
            this.lnkReset.AutoSize = true;
            this.lnkReset.BackColor = System.Drawing.Color.Transparent;
            this.lnkReset.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkReset.Location = new System.Drawing.Point(24, 167);
            this.lnkReset.Name = "lnkReset";
            this.lnkReset.Size = new System.Drawing.Size(138, 19);
            this.lnkReset.TabIndex = 20;
            this.lnkReset.TabStop = true;
            this.lnkReset.Text = "Reset User Login";
            this.lnkReset.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkReset_LinkClicked);
            // 
            // clsSeparator3
            // 
            this.clsSeparator3.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator3.Location = new System.Drawing.Point(17, 194);
            this.clsSeparator3.Name = "clsSeparator3";
            this.clsSeparator3.Size = new System.Drawing.Size(307, 2);
            this.clsSeparator3.TabIndex = 19;
            this.clsSeparator3.TabStop = false;
            // 
            // lbl_Login
            // 
            this.lbl_Login.AutoSize = true;
            this.lbl_Login.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Login.Location = new System.Drawing.Point(24, 35);
            this.lbl_Login.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Login.Name = "lbl_Login";
            this.lbl_Login.Size = new System.Drawing.Size(94, 20);
            this.lbl_Login.TabIndex = 7;
            this.lbl_Login.Text = "Login User";
            // 
            // TxtUserName
            // 
            this.TxtUserName.BackColor = System.Drawing.SystemColors.Window;
            this.TxtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUserName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TxtUserName.Location = new System.Drawing.Point(24, 60);
            this.TxtUserName.Margin = new System.Windows.Forms.Padding(4);
            this.TxtUserName.Name = "TxtUserName";
            this.TxtUserName.Size = new System.Drawing.Size(298, 29);
            this.TxtUserName.TabIndex = 0;
            this.TxtUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtUserName_KeyDown);
            this.TxtUserName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            this.TxtUserName.Validating += new System.ComponentModel.CancelEventHandler(this.TxtUserName_Validating);
            // 
            // TxtUserPassword
            // 
            this.TxtUserPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUserPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUserPassword.Location = new System.Drawing.Point(24, 127);
            this.TxtUserPassword.Margin = new System.Windows.Forms.Padding(4);
            this.TxtUserPassword.Name = "TxtUserPassword";
            this.TxtUserPassword.PasswordChar = 'X';
            this.TxtUserPassword.Size = new System.Drawing.Size(298, 29);
            this.TxtUserPassword.TabIndex = 1;
            this.TxtUserPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtUserPassword_KeyDown);
            this.TxtUserPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            this.TxtUserPassword.Validating += new System.ComponentModel.CancelEventHandler(this.TxtUserPassword_Validating);
            // 
            // BtnLogin
            // 
            this.BtnLogin.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 16F);
            this.BtnLogin.Appearance.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BtnLogin.Appearance.Options.UseFont = true;
            this.BtnLogin.Appearance.Options.UseForeColor = true;
            this.BtnLogin.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.BtnLogin.ImageOptions.Image = global::MrBLL.Properties.Resources.logout;
            this.BtnLogin.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnLogin.Location = new System.Drawing.Point(72, 207);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(203, 49);
            this.BtnLogin.TabIndex = 2;
            this.BtnLogin.Text = "&LOGIN";
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 99);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Password";
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.mrGroup2);
            this.mrPanel1.Controls.Add(this.mrGroup1);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(0, 0);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(364, 443);
            this.mrPanel1.TabIndex = 20;
            // 
            // mrGroup2
            // 
            this.mrGroup2.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup2.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup2.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup2.BorderColor = System.Drawing.Color.White;
            this.mrGroup2.BorderThickness = 1F;
            this.mrGroup2.Controls.Add(this.lbl_Login);
            this.mrGroup2.Controls.Add(this.TxtUserPassword);
            this.mrGroup2.Controls.Add(this.lnkReset);
            this.mrGroup2.Controls.Add(this.BtnLogin);
            this.mrGroup2.Controls.Add(this.clsSeparator3);
            this.mrGroup2.Controls.Add(this.label1);
            this.mrGroup2.Controls.Add(this.TxtUserName);
            this.mrGroup2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mrGroup2.GroupImage = null;
            this.mrGroup2.GroupTitle = "User Info";
            this.mrGroup2.Location = new System.Drawing.Point(0, 159);
            this.mrGroup2.Name = "mrGroup2";
            this.mrGroup2.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup2.PaintGroupBox = false;
            this.mrGroup2.RoundCorners = 10;
            this.mrGroup2.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup2.ShadowControl = true;
            this.mrGroup2.ShadowThickness = 3;
            this.mrGroup2.Size = new System.Drawing.Size(364, 284);
            this.mrGroup2.TabIndex = 25;
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.LblSoftwareSpec);
            this.mrGroup1.Controls.Add(this.LblSoftwareCaption);
            this.mrGroup1.Controls.Add(this.pictureBox1);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.Dock = System.Windows.Forms.DockStyle.Top;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "";
            this.mrGroup1.Location = new System.Drawing.Point(0, 0);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 10;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = true;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(364, 161);
            this.mrGroup1.TabIndex = 24;
            // 
            // LblSoftwareSpec
            // 
            this.LblSoftwareSpec.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LblSoftwareSpec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblSoftwareSpec.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSoftwareSpec.Location = new System.Drawing.Point(154, 64);
            this.LblSoftwareSpec.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblSoftwareSpec.Name = "LblSoftwareSpec";
            this.LblSoftwareSpec.Size = new System.Drawing.Size(190, 74);
            this.LblSoftwareSpec.TabIndex = 24;
            this.LblSoftwareSpec.Text = "Welcome, MrSolution Enter Your User Name && Password";
            // 
            // LblSoftwareCaption
            // 
            this.LblSoftwareCaption.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LblSoftwareCaption.AutoSize = true;
            this.LblSoftwareCaption.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSoftwareCaption.ForeColor = System.Drawing.Color.DarkBlue;
            this.LblSoftwareCaption.Location = new System.Drawing.Point(163, 27);
            this.LblSoftwareCaption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblSoftwareCaption.Name = "LblSoftwareCaption";
            this.LblSoftwareCaption.Size = new System.Drawing.Size(154, 20);
            this.LblSoftwareCaption.TabIndex = 23;
            this.LblSoftwareCaption.Text =  "MrSolution V2018";
            // 
            // pictureBox1
            // 
            // this.pictureBox1.Image = global::MrBLL.Properties.Resources.AccountIcon1;
            this.pictureBox1.Image =  Properties.Resources.AccountIcon1;
            this.pictureBox1.Location = new System.Drawing.Point(8, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(138, 138);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(364, 443);
            this.ControlBox = false;
            this.Controls.Add(this.mrPanel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.Text = "USER LOGIN INFO";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLogin_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmLogin_FormClosed);
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.Shown += new System.EventHandler(this.FrmLogin_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmLogin_KeyDown);
            this.mrPanel1.ResumeLayout(false);
            this.mrGroup2.ResumeLayout(false);
            this.mrGroup2.PerformLayout();
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_Login;
        private DevExpress.XtraEditors.SimpleButton BtnLogin;
        private System.Windows.Forms.Label label1;
#pragma warning disable CS0169 // The field 'FrmLogin.clsSeparator1' is never used
        private ClsSeparator clsSeparator1;
#pragma warning restore CS0169 // The field 'FrmLogin.clsSeparator1' is never used
#pragma warning disable CS0169 // The field 'FrmLogin.clsSeparator2' is never used
        private ClsSeparator clsSeparator2;
#pragma warning restore CS0169 // The field 'FrmLogin.clsSeparator2' is never used
        private ClsSeparator clsSeparator3;
        private System.Windows.Forms.LinkLabel lnkReset;
        private MrTextBox TxtUserPassword;
        private MrTextBox TxtUserName;
        private MrPanel mrPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label LblSoftwareCaption;
        private MrGroup mrGroup1;
        private System.Windows.Forms.Label LblSoftwareSpec;
        private MrGroup mrGroup2;
    }
}