
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.POS.Master
{
    partial class FrmLockScreen
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
            this.mrGroup1 = new MrGroup();
            this.BtnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.TxtUserPassword = new MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.mrGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.BtnLogin);
            this.mrGroup1.Controls.Add(this.TxtUserPassword);
            this.mrGroup1.Controls.Add(this.label1);
            this.mrGroup1.Controls.Add(this.pictureBox1);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "LOCK SCREEN";
            this.mrGroup1.Location = new System.Drawing.Point(0, 0);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 10;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(307, 427);
            this.mrGroup1.TabIndex = 0;
            this.mrGroup1.UseWaitCursor = true;
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
            this.BtnLogin.Location = new System.Drawing.Point(75, 381);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(156, 43);
            this.BtnLogin.TabIndex = 23;
            this.BtnLogin.Text = "&LOGIN";
            this.BtnLogin.UseWaitCursor = true;
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // TxtUserPassword
            // 
            this.TxtUserPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUserPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUserPassword.Location = new System.Drawing.Point(57, 349);
            this.TxtUserPassword.Margin = new System.Windows.Forms.Padding(4);
            this.TxtUserPassword.Name = "TxtUserPassword";
            this.TxtUserPassword.PasswordChar = '*';
            this.TxtUserPassword.Size = new System.Drawing.Size(192, 29);
            this.TxtUserPassword.TabIndex = 21;
            this.TxtUserPassword.UseWaitCursor = true;
            this.TxtUserPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtUserPassword_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(114, 326);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 19);
            this.label1.TabIndex = 22;
            this.label1.Text = "Password";
            this.label1.UseWaitCursor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MrBLL.Properties.Resources.LoginImage;
            this.pictureBox1.Location = new System.Drawing.Point(26, 38);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(255, 281);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.UseWaitCursor = true;
            // 
            // FrmLockScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(307, 427);
            this.ControlBox = false;
            this.Controls.Add(this.mrGroup1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLockScreen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LOCK SCREEN";
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.FrmLockScreen_Load);
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MrGroup mrGroup1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton BtnLogin;
        private MrTextBox TxtUserPassword;
    }
}