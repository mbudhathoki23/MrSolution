using MrDAL.Control.ControlsEx.Control;

namespace MrSolution.About
{
    partial class FrmAboutUs
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAboutUs));
            this.labelProductName = new System.Windows.Forms.Label();
            this.labelCompanyName = new System.Windows.Forms.Label();
            this.TxtProductDesc = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.PanelHeader = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.LblSoftwareVersion = new System.Windows.Forms.Label();
            this.labelCopyright = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.PanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelProductName
            // 
            this.labelProductName.AutoSize = true;
            this.labelProductName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.labelProductName.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProductName.Location = new System.Drawing.Point(6, 348);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(107, 19);
            this.labelProductName.TabIndex = 19;
            this.labelProductName.Text = "Product Name";
            this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCompanyName
            // 
            this.labelCompanyName.AutoSize = true;
            this.labelCompanyName.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCompanyName.Location = new System.Drawing.Point(6, 423);
            this.labelCompanyName.Name = "labelCompanyName";
            this.labelCompanyName.Size = new System.Drawing.Size(117, 19);
            this.labelCompanyName.TabIndex = 22;
            this.labelCompanyName.Text = "Company Name";
            this.labelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtProductDesc
            // 
            this.TxtProductDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtProductDesc.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.TxtProductDesc.Location = new System.Drawing.Point(9, 449);
            this.TxtProductDesc.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.TxtProductDesc.Multiline = true;
            this.TxtProductDesc.Name = "TxtProductDesc";
            this.TxtProductDesc.ReadOnly = true;
            this.TxtProductDesc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtProductDesc.Size = new System.Drawing.Size(606, 207);
            this.TxtProductDesc.TabIndex = 23;
            this.TxtProductDesc.TabStop = false;
            this.TxtProductDesc.Text = "Description";
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.Color.IndianRed;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Location = new System.Drawing.Point(533, 663);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 34);
            this.okButton.TabIndex = 24;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = false;
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Image = global::MrSolution.Properties.Resources.Splash11;
            this.logoPictureBox.Location = new System.Drawing.Point(6, 17);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(609, 328);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPictureBox.TabIndex = 12;
            this.logoPictureBox.TabStop = false;
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.webView21);
            this.PanelHeader.Controls.Add(this.groupBox1);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(9, 9);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(1642, 702);
            this.PanelHeader.TabIndex = 40;
            // 
            // webView21
            // 
            this.webView21.AllowExternalDrop = true;
            this.webView21.CreationProperties = null;
            this.webView21.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView21.Location = new System.Drawing.Point(624, 0);
            this.webView21.Name = "webView21";
            this.webView21.Size = new System.Drawing.Size(1018, 702);
            this.webView21.Source = new System.Uri("https://www.facebook.com/MrSolutionERP/", System.UriKind.Absolute);
            this.webView21.TabIndex = 36;
            this.webView21.ZoomFactor = 1D;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.logoPictureBox);
            this.groupBox1.Controls.Add(this.labelProductName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.labelCompanyName);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.okButton);
            this.groupBox1.Controls.Add(this.LblSoftwareVersion);
            this.groupBox1.Controls.Add(this.TxtProductDesc);
            this.groupBox1.Controls.Add(this.labelCopyright);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(624, 702);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(222, 668);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 20);
            this.label1.TabIndex = 35;
            this.label1.Text = "mbudhathoki23@gmail.com";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 668);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(201, 20);
            this.label7.TabIndex = 34;
            this.label7.Text = "info@mrsolution.com.np";
            // 
            // LblSoftwareVersion
            // 
            this.LblSoftwareVersion.AutoSize = true;
            this.LblSoftwareVersion.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSoftwareVersion.Location = new System.Drawing.Point(6, 373);
            this.LblSoftwareVersion.Name = "LblSoftwareVersion";
            this.LblSoftwareVersion.Size = new System.Drawing.Size(126, 19);
            this.LblSoftwareVersion.TabIndex = 26;
            this.LblSoftwareVersion.Text = "Software Version";
            this.LblSoftwareVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCopyright
            // 
            this.labelCopyright.AutoSize = true;
            this.labelCopyright.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCopyright.Location = new System.Drawing.Point(6, 398);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(76, 19);
            this.labelCopyright.TabIndex = 21;
            this.labelCopyright.Text = "Copyright";
            this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmAboutUs
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1660, 720);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAboutUs";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.Text = "About Company Info";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AboutBox1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.PanelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelCompanyName;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Label LblSoftwareVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private MrTextBox TxtProductDesc;
        private MrPanel PanelHeader;
        private System.Windows.Forms.Label labelCopyright;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
