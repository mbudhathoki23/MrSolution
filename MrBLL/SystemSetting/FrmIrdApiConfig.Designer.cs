using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.SystemSetting
{
    partial class FrmIrdApiConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIrdApiConfig));
            this.panel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.BtnLocation = new System.Windows.Forms.Button();
            this.TxtLocation = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtApiAddress = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.ChkOnlineSync = new System.Windows.Forms.CheckBox();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.label35 = new System.Windows.Forms.Label();
            this.TxtPassword = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtPanNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtUserName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.SaveLocation = new DevExpress.XtraEditors.XtraFolderBrowserDialog(this.components);
            this.panel1.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.TabControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(452, 227);
            this.panel1.TabIndex = 0;
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.tabPage1);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(452, 227);
            this.TabControl.TabIndex = 230;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.BtnLocation);
            this.tabPage1.Controls.Add(this.TxtLocation);
            this.tabPage1.Controls.Add(this.TxtApiAddress);
            this.tabPage1.Controls.Add(this.BtnCancel);
            this.tabPage1.Controls.Add(this.ChkOnlineSync);
            this.tabPage1.Controls.Add(this.BtnSave);
            this.tabPage1.Controls.Add(this.label35);
            this.tabPage1.Controls.Add(this.TxtPassword);
            this.tabPage1.Controls.Add(this.TxtPanNo);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.TxtUserName);
            this.tabPage1.Controls.Add(this.clsSeparator1);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(444, 195);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Ird API Config";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 117);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(69, 19);
            this.label4.TabIndex = 232;
            this.label4.Text = "Location";
            // 
            // BtnLocation
            // 
            this.BtnLocation.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnLocation.Location = new System.Drawing.Point(416, 117);
            this.BtnLocation.Name = "BtnLocation";
            this.BtnLocation.Size = new System.Drawing.Size(24, 23);
            this.BtnLocation.TabIndex = 231;
            this.BtnLocation.UseVisualStyleBackColor = true;
            this.BtnLocation.Click += new System.EventHandler(this.BtnLocation_Click);
            // 
            // TxtLocation
            // 
            this.TxtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtLocation.Location = new System.Drawing.Point(97, 115);
            this.TxtLocation.Name = "TxtLocation";
            this.TxtLocation.Size = new System.Drawing.Size(315, 25);
            this.TxtLocation.TabIndex = 230;
            // 
            // TxtApiAddress
            // 
            this.TxtApiAddress.BackColor = System.Drawing.Color.White;
            this.TxtApiAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtApiAddress.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtApiAddress.ForeColor = System.Drawing.Color.Black;
            this.TxtApiAddress.Location = new System.Drawing.Point(97, 6);
            this.TxtApiAddress.MaxLength = 255;
            this.TxtApiAddress.Name = "TxtApiAddress";
            this.TxtApiAddress.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtApiAddress.Size = new System.Drawing.Size(335, 23);
            this.TxtApiAddress.TabIndex = 218;
            this.TxtApiAddress.Text = "https://cbapi.ird.gov.np/api/bill";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.Appearance.Options.UseForeColor = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(255, 156);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(103, 34);
            this.BtnCancel.TabIndex = 228;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // ChkOnlineSync
            // 
            this.ChkOnlineSync.AutoSize = true;
            this.ChkOnlineSync.Checked = true;
            this.ChkOnlineSync.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkOnlineSync.Location = new System.Drawing.Point(14, 156);
            this.ChkOnlineSync.Name = "ChkOnlineSync";
            this.ChkOnlineSync.Size = new System.Drawing.Size(120, 23);
            this.ChkOnlineSync.TabIndex = 229;
            this.ChkOnlineSync.Text = "Online Sync";
            this.ChkOnlineSync.UseVisualStyleBackColor = true;
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(163, 156);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(91, 34);
            this.BtnSave.TabIndex = 227;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(6, 8);
            this.label35.Name = "label35";
            this.label35.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label35.Size = new System.Drawing.Size(81, 19);
            this.label35.TabIndex = 219;
            this.label35.Text = "API Server";
            // 
            // TxtPassword
            // 
            this.TxtPassword.BackColor = System.Drawing.Color.White;
            this.TxtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPassword.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPassword.Location = new System.Drawing.Point(97, 60);
            this.TxtPassword.MaxLength = 255;
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtPassword.Size = new System.Drawing.Size(157, 23);
            this.TxtPassword.TabIndex = 222;
            this.TxtPassword.Text = "test@321";
            // 
            // TxtPanNo
            // 
            this.TxtPanNo.BackColor = System.Drawing.Color.White;
            this.TxtPanNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPanNo.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPanNo.Location = new System.Drawing.Point(97, 86);
            this.TxtPanNo.MaxLength = 255;
            this.TxtPanNo.Name = "TxtPanNo";
            this.TxtPanNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtPanNo.Size = new System.Drawing.Size(157, 23);
            this.TxtPanNo.TabIndex = 224;
            this.TxtPanNo.Text = "999999999";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 36);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(83, 19);
            this.label1.TabIndex = 221;
            this.label1.Text = "User Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 88);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(54, 19);
            this.label3.TabIndex = 225;
            this.label3.Text = "PanNo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 62);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(74, 19);
            this.label2.TabIndex = 223;
            this.label2.Text = "Password";
            // 
            // TxtUserName
            // 
            this.TxtUserName.BackColor = System.Drawing.Color.White;
            this.TxtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUserName.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUserName.ForeColor = System.Drawing.Color.Black;
            this.TxtUserName.Location = new System.Drawing.Point(97, 34);
            this.TxtUserName.MaxLength = 255;
            this.TxtUserName.Name = "TxtUserName";
            this.TxtUserName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtUserName.Size = new System.Drawing.Size(157, 23);
            this.TxtUserName.TabIndex = 220;
            this.TxtUserName.Text = "Test_CBMS";
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(7, 148);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(425, 2);
            this.clsSeparator1.TabIndex = 226;
            this.clsSeparator1.TabStop = false;
            // 
            // SaveLocation
            // 
            this.SaveLocation.SelectedPath = "SaveLocation";
            // 
            // FrmIrdApiConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 227);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmIrdApiConfig";
            this.ShowIcon = false;
            this.Text = "IRD CONFIG";
            this.Load += new System.EventHandler(this.FrmIrdApiConfig_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmIrdApiConfig_KeyPress);
            this.panel1.ResumeLayout(false);
            this.TabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private System.Windows.Forms.CheckBox ChkOnlineSync;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private MrTextBox TxtApiAddress;
        private System.Windows.Forms.Label label35;
        private MrTextBox TxtPassword;
        private MrTextBox TxtPanNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private MrTextBox TxtUserName;
        private ClsSeparator clsSeparator1;
        private MrPanel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BtnLocation;
        private MrTextBox TxtLocation;
        private DevExpress.XtraEditors.XtraFolderBrowserDialog SaveLocation;
    }
}