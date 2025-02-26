using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Setup.UserSetup
{
    partial class FrmOnlineUser
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
			this.roundPanel3 = new RoundPanel();
			this.clsSeparator3 = new ClsSeparator();
			this.lbl_Login = new System.Windows.Forms.Label();
			this.TxtUserName = new MrTextBox();
			this.button3 = new System.Windows.Forms.Button();
			this.TxtUserPassword = new MrTextBox();
			this.BtnLogin = new DevExpress.XtraEditors.SimpleButton();
			this.label1 = new System.Windows.Forms.Label();
			this.button4 = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.roundPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// roundPanel3
			// 
			this.roundPanel3.BackColor = System.Drawing.Color.Transparent;
			this.roundPanel3.Controls.Add(this.clsSeparator3);
			this.roundPanel3.Controls.Add(this.lbl_Login);
			this.roundPanel3.Controls.Add(this.TxtUserName);
			this.roundPanel3.Controls.Add(this.button3);
			this.roundPanel3.Controls.Add(this.TxtUserPassword);
			this.roundPanel3.Controls.Add(this.BtnLogin);
			this.roundPanel3.Controls.Add(this.label1);
			this.roundPanel3.Controls.Add(this.button4);
			this.roundPanel3.Location = new System.Drawing.Point(5, 256);
			this.roundPanel3.Name = "roundPanel3";
			this.roundPanel3.Radious = 25;
			this.roundPanel3.Size = new System.Drawing.Size(307, 158);
			this.roundPanel3.TabIndex = 20;
			this.roundPanel3.TabStop = false;
			this.roundPanel3.Text = "USER INFO";
			this.roundPanel3.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
			this.roundPanel3.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 16.25F, System.Drawing.FontStyle.Bold);
			this.roundPanel3.TitleForeColor = System.Drawing.Color.White;
			this.roundPanel3.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
			// 
			// clsSeparator3
			// 
			this.clsSeparator3.BackColor = System.Drawing.Color.Lavender;
			this.clsSeparator3.Location = new System.Drawing.Point(3, 105);
			this.clsSeparator3.Name = "clsSeparator3";
			this.clsSeparator3.Size = new System.Drawing.Size(273, 3);
			this.clsSeparator3.TabIndex = 19;
			this.clsSeparator3.TabStop = false;
			// 
			// lbl_Login
			// 
			this.lbl_Login.AutoSize = true;
			this.lbl_Login.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_Login.Location = new System.Drawing.Point(40, 46);
			this.lbl_Login.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbl_Login.Name = "lbl_Login";
			this.lbl_Login.Size = new System.Drawing.Size(90, 19);
			this.lbl_Login.TabIndex = 7;
			this.lbl_Login.Text = "Login User";
			// 
			// TxtUserName
			// 
			this.TxtUserName.BackColor = System.Drawing.SystemColors.Window;
			this.TxtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtUserName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.TxtUserName.Location = new System.Drawing.Point(141, 39);
			this.TxtUserName.Margin = new System.Windows.Forms.Padding(4);
			this.TxtUserName.Name = "TxtUserName";
			this.TxtUserName.Size = new System.Drawing.Size(156, 29);
			this.TxtUserName.TabIndex = 0;
			// 
			// button3
			// 
			this.button3.CausesValidation = false;
			this.button3.Enabled = false;
			this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button3.Image = global::MrBLL.Properties.Resources.Lock;
			this.button3.Location = new System.Drawing.Point(8, 41);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(31, 29);
			this.button3.TabIndex = 17;
			this.button3.UseVisualStyleBackColor = true;
			// 
			// TxtUserPassword
			// 
			this.TxtUserPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtUserPassword.Location = new System.Drawing.Point(141, 74);
			this.TxtUserPassword.Margin = new System.Windows.Forms.Padding(4);
			this.TxtUserPassword.Name = "TxtUserPassword";
			this.TxtUserPassword.PasswordChar = 'X';
			this.TxtUserPassword.Size = new System.Drawing.Size(156, 29);
			this.TxtUserPassword.TabIndex = 1;
			// 
			// BtnLogin
			// 
			this.BtnLogin.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
			this.BtnLogin.Appearance.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.BtnLogin.Appearance.Options.UseFont = true;
			this.BtnLogin.Appearance.Options.UseForeColor = true;
			this.BtnLogin.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
			this.BtnLogin.ImageOptions.Image = global::MrBLL.Properties.Resources.logout;
			this.BtnLogin.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
			this.BtnLogin.Location = new System.Drawing.Point(144, 111);
			this.BtnLogin.Name = "BtnLogin";
			this.BtnLogin.Size = new System.Drawing.Size(132, 38);
			this.BtnLogin.TabIndex = 2;
			this.BtnLogin.Text = "&LOGIN";
			this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(40, 81);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 19);
			this.label1.TabIndex = 14;
			this.label1.Text = "Password";
			// 
			// button4
			// 
			this.button4.CausesValidation = false;
			this.button4.Enabled = false;
			this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button4.Image = global::MrBLL.Properties.Resources.Password;
			this.button4.Location = new System.Drawing.Point(8, 78);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(31, 25);
			this.button4.TabIndex = 18;
			this.button4.UseVisualStyleBackColor = true;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::MrBLL.Properties.Resources.LoginImage;
			this.pictureBox1.Location = new System.Drawing.Point(37, 5);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(244, 245);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 21;
			this.pictureBox1.TabStop = false;
			// 
			// FrmOnlineUser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(319, 419);
			this.Controls.Add(this.roundPanel3);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.Name = "FrmOnlineUser";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ONLINE USER VERIFCATION";
			this.Load += new System.EventHandler(this.FrmOnlineUser_Load);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmOnlineUser_KeyPress);
			this.roundPanel3.ResumeLayout(false);
			this.roundPanel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private RoundPanel roundPanel3;
		private ClsSeparator clsSeparator3;
		private System.Windows.Forms.Label lbl_Login;
		private System.Windows.Forms.TextBox TxtUserName;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TextBox TxtUserPassword;
		private DevExpress.XtraEditors.SimpleButton BtnLogin;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}