using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Setup.UserSetup
{
    partial class FrmChangePassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChangePassword));
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnChange = new DevExpress.XtraEditors.SimpleButton();
            this.TxtNewPassword = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtConfirmPassword = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtOldPassword = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_ConfirmPassword = new System.Windows.Forms.Label();
            this.lbl_OldPassword = new System.Windows.Forms.Label();
            this.PanelHeader = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.mrGroup2 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.mrGroup1 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PanelHeader.SuspendLayout();
            this.mrGroup2.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(133, 222);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(176, 46);
            this.BtnCancel.TabIndex = 4;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnChange
            // 
            this.BtnChange.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.BtnChange.Appearance.Options.UseFont = true;
            this.BtnChange.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnChange.Location = new System.Drawing.Point(89, 152);
            this.BtnChange.Name = "BtnChange";
            this.BtnChange.Size = new System.Drawing.Size(270, 64);
            this.BtnChange.TabIndex = 3;
            this.BtnChange.Text = "CHA&NGE";
            this.BtnChange.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // TxtNewPassword
            // 
            this.TxtNewPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNewPassword.Font = new System.Drawing.Font("Bookman Old Style", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNewPassword.Location = new System.Drawing.Point(162, 64);
            this.TxtNewPassword.Name = "TxtNewPassword";
            this.TxtNewPassword.PasswordChar = '*';
            this.TxtNewPassword.Size = new System.Drawing.Size(202, 33);
            this.TxtNewPassword.TabIndex = 1;
            this.TxtNewPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtNewPassword_KeyDown);
            this.TxtNewPassword.Validating += new System.ComponentModel.CancelEventHandler(this.TxtNewPassword_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 20);
            this.label3.TabIndex = 36;
            this.label3.Text = "New Password";
            // 
            // TxtConfirmPassword
            // 
            this.TxtConfirmPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtConfirmPassword.Font = new System.Drawing.Font("Bookman Old Style", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtConfirmPassword.Location = new System.Drawing.Point(162, 101);
            this.TxtConfirmPassword.Name = "TxtConfirmPassword";
            this.TxtConfirmPassword.PasswordChar = '*';
            this.TxtConfirmPassword.Size = new System.Drawing.Size(202, 33);
            this.TxtConfirmPassword.TabIndex = 2;
            this.TxtConfirmPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtConfirmPassword_KeyDown);
            this.TxtConfirmPassword.Validating += new System.ComponentModel.CancelEventHandler(this.TxtConfirmPassword_Validating);
            // 
            // TxtOldPassword
            // 
            this.TxtOldPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtOldPassword.Font = new System.Drawing.Font("Bookman Old Style", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtOldPassword.Location = new System.Drawing.Point(162, 26);
            this.TxtOldPassword.Name = "TxtOldPassword";
            this.TxtOldPassword.PasswordChar = '*';
            this.TxtOldPassword.Size = new System.Drawing.Size(202, 33);
            this.TxtOldPassword.TabIndex = 0;
            this.TxtOldPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtOldPassword_KeyDown);
            this.TxtOldPassword.Validating += new System.ComponentModel.CancelEventHandler(this.TxtOldPassword_Validating);
            // 
            // lbl_ConfirmPassword
            // 
            this.lbl_ConfirmPassword.AutoSize = true;
            this.lbl_ConfirmPassword.Location = new System.Drawing.Point(5, 107);
            this.lbl_ConfirmPassword.Name = "lbl_ConfirmPassword";
            this.lbl_ConfirmPassword.Size = new System.Drawing.Size(151, 20);
            this.lbl_ConfirmPassword.TabIndex = 1;
            this.lbl_ConfirmPassword.Text = "Confirm Password";
            // 
            // lbl_OldPassword
            // 
            this.lbl_OldPassword.AutoSize = true;
            this.lbl_OldPassword.Location = new System.Drawing.Point(5, 32);
            this.lbl_OldPassword.Name = "lbl_OldPassword";
            this.lbl_OldPassword.Size = new System.Drawing.Size(115, 20);
            this.lbl_OldPassword.TabIndex = 0;
            this.lbl_OldPassword.Text = "Old Password";
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.mrGroup2);
            this.PanelHeader.Controls.Add(this.mrGroup1);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(382, 435);
            this.PanelHeader.TabIndex = 0;
            // 
            // mrGroup2
            // 
            this.mrGroup2.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup2.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup2.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup2.BorderColor = System.Drawing.Color.White;
            this.mrGroup2.BorderThickness = 1F;
            this.mrGroup2.Controls.Add(this.lbl_OldPassword);
            this.mrGroup2.Controls.Add(this.TxtOldPassword);
            this.mrGroup2.Controls.Add(this.clsSeparator1);
            this.mrGroup2.Controls.Add(this.label3);
            this.mrGroup2.Controls.Add(this.BtnCancel);
            this.mrGroup2.Controls.Add(this.TxtNewPassword);
            this.mrGroup2.Controls.Add(this.BtnChange);
            this.mrGroup2.Controls.Add(this.TxtConfirmPassword);
            this.mrGroup2.Controls.Add(this.lbl_ConfirmPassword);
            this.mrGroup2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup2.GroupImage = null;
            this.mrGroup2.GroupTitle = "User Info";
            this.mrGroup2.Location = new System.Drawing.Point(3, 150);
            this.mrGroup2.Name = "mrGroup2";
            this.mrGroup2.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup2.PaintGroupBox = false;
            this.mrGroup2.RoundCorners = 10;
            this.mrGroup2.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup2.ShadowControl = true;
            this.mrGroup2.ShadowThickness = 3;
            this.mrGroup2.Size = new System.Drawing.Size(376, 282);
            this.mrGroup2.TabIndex = 0;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(6, 139);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(358, 2);
            this.clsSeparator1.TabIndex = 37;
            this.clsSeparator1.TabStop = false;
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.label4);
            this.mrGroup1.Controls.Add(this.label2);
            this.mrGroup1.Controls.Add(this.pictureBox1);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "";
            this.mrGroup1.Location = new System.Drawing.Point(3, 3);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 10;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = true;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(376, 146);
            this.mrGroup1.TabIndex = 38;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(160, 57);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(190, 74);
            this.label4.TabIndex = 24;
            this.label4.Text = "Welcome, MrSolution Enter Your User Name New Password";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MrBLL.Properties.Resources.Mricon;
            this.pictureBox1.Location = new System.Drawing.Point(9, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(131, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(169, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 20);
            this.label2.TabIndex = 23;
            this.label2.Text = "MrSolution V2018";
            // 
            // FrmChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(382, 435);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmChangePassword";
            this.ShowIcon = false;
            this.Text = "Change LogIn Password";
            this.Load += new System.EventHandler(this.FrmChangePassword_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmChangePassword_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            this.mrGroup2.ResumeLayout(false);
            this.mrGroup2.PerformLayout();
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_ConfirmPassword;
        private System.Windows.Forms.Label lbl_OldPassword;
        private DevExpress.XtraEditors.SimpleButton BtnChange;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private ClsSeparator clsSeparator1;
        private MrTextBox TxtNewPassword;
        private MrTextBox TxtConfirmPassword;
        private MrTextBox TxtOldPassword;
        private MrPanel PanelHeader;
        private MrGroup mrGroup2;
        private MrGroup mrGroup1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
    }
}