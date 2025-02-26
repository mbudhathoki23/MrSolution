
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Setup.UserSetup
{
    partial class FrmUserReset
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUserReset));
            this.roundPanel3 = new RoundPanel();
            this.clsSeparator3 = new ClsSeparator();
            this.lbl_Login = new System.Windows.Forms.Label();
            this.TxtUserName = new MrTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.TxtUserPassword = new MrTextBox();
            this.BtnReset = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.roundPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // roundPanel3
            // 
            this.roundPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.roundPanel3.BackColor = System.Drawing.Color.Transparent;
            this.roundPanel3.Controls.Add(this.clsSeparator3);
            this.roundPanel3.Controls.Add(this.lbl_Login);
            this.roundPanel3.Controls.Add(this.TxtUserName);
            this.roundPanel3.Controls.Add(this.button3);
            this.roundPanel3.Controls.Add(this.TxtUserPassword);
            this.roundPanel3.Controls.Add(this.BtnReset);
            this.roundPanel3.Controls.Add(this.label1);
            this.roundPanel3.Controls.Add(this.button4);
            this.roundPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roundPanel3.Location = new System.Drawing.Point(0, 0);
            this.roundPanel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.roundPanel3.Name = "roundPanel3";
            this.roundPanel3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.roundPanel3.Radious = 25;
            this.roundPanel3.Size = new System.Drawing.Size(323, 209);
            this.roundPanel3.TabIndex = 1;
            this.roundPanel3.TabStop = false;
            this.roundPanel3.Text = "RESET USER";
            this.roundPanel3.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel3.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roundPanel3.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel3.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            // 
            // clsSeparator3
            // 
            this.clsSeparator3.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator3.Location = new System.Drawing.Point(4, 143);
            this.clsSeparator3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clsSeparator3.Name = "clsSeparator3";
            this.clsSeparator3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clsSeparator3.Size = new System.Drawing.Size(313, 2);
            this.clsSeparator3.TabIndex = 19;
            this.clsSeparator3.TabStop = false;
            // 
            // lbl_Login
            // 
            this.lbl_Login.AutoSize = true;
            this.lbl_Login.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Login.Location = new System.Drawing.Point(55, 54);
            this.lbl_Login.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbl_Login.Name = "lbl_Login";
            this.lbl_Login.Size = new System.Drawing.Size(80, 18);
            this.lbl_Login.TabIndex = 7;
            this.lbl_Login.Text = "Login User";
            // 
            // TxtUserName
            // 
            this.TxtUserName.BackColor = System.Drawing.SystemColors.Window;
            this.TxtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUserName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TxtUserName.Location = new System.Drawing.Point(140, 49);
            this.TxtUserName.Margin = new System.Windows.Forms.Padding(6);
            this.TxtUserName.Name = "TxtUserName";
            this.TxtUserName.Size = new System.Drawing.Size(168, 29);
            this.TxtUserName.TabIndex = 0;
            this.TxtUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtUserName_KeyDown);
            this.TxtUserName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // button3
            // 
            this.button3.CausesValidation = false;
            this.button3.Enabled = false;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Image = global::MrBLL.Properties.Resources.Lock;
            this.button3.Location = new System.Drawing.Point(12, 45);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(37, 35);
            this.button3.TabIndex = 17;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // TxtUserPassword
            // 
            this.TxtUserPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUserPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUserPassword.Location = new System.Drawing.Point(140, 99);
            this.TxtUserPassword.Margin = new System.Windows.Forms.Padding(6);
            this.TxtUserPassword.Name = "TxtUserPassword";
            this.TxtUserPassword.PasswordChar = 'X';
            this.TxtUserPassword.Size = new System.Drawing.Size(168, 29);
            this.TxtUserPassword.TabIndex = 1;
            this.TxtUserPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // BtnReset
            // 
            this.BtnReset.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.BtnReset.Appearance.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BtnReset.Appearance.Options.UseFont = true;
            this.BtnReset.Appearance.Options.UseForeColor = true;
            this.BtnReset.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.BtnReset.ImageOptions.Image = global::MrBLL.Properties.Resources.logout;
            this.BtnReset.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnReset.Location = new System.Drawing.Point(157, 154);
            this.BtnReset.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(132, 43);
            this.BtnReset.TabIndex = 2;
            this.BtnReset.Text = "&RESET";
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(55, 105);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 18);
            this.label1.TabIndex = 14;
            this.label1.Text = "Password";
            // 
            // button4
            // 
            this.button4.CausesValidation = false;
            this.button4.Enabled = false;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Image = global::MrBLL.Properties.Resources.Password;
            this.button4.Location = new System.Drawing.Point(12, 98);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(37, 35);
            this.button4.TabIndex = 18;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // FrmUserReset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 209);
            this.Controls.Add(this.roundPanel3);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUserReset";
            this.Text = "RESET LOGIN USER";
            this.Load += new System.EventHandler(this.FrmUserReset_Load);
            this.roundPanel3.ResumeLayout(false);
            this.roundPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private RoundPanel roundPanel3;
        private ClsSeparator clsSeparator3;
        private System.Windows.Forms.Label lbl_Login;
        private System.Windows.Forms.Button button3;
        private DevExpress.XtraEditors.SimpleButton BtnReset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private MrTextBox TxtUserName;
        private MrTextBox TxtUserPassword;
    }
}