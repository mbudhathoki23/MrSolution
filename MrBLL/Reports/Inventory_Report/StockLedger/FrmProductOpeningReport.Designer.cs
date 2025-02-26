using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Inventory_Report.StockLedger
{
    partial class FrmProductOpeningReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProductOpeningReport));
            this.clsSeparator1 = new ClsSeparator();
            this.ChkSelectAll = new System.Windows.Forms.CheckBox();
            this.StorePanel = new MrPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rChkProductDetails = new System.Windows.Forms.RadioButton();
            this.rChkVoucherDetails = new System.Windows.Forms.RadioButton();
            this.rChkOpeningOnly = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rChkGodownWise = new System.Windows.Forms.RadioButton();
            this.rChkCompanyWise = new System.Windows.Forms.RadioButton();
            this.rChkNormal = new System.Windows.Forms.RadioButton();
            this.rChkSubGroupWise = new System.Windows.Forms.RadioButton();
            this.rChkGroupWise = new System.Windows.Forms.RadioButton();
            this.btn_Show = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.mrGroup1 = new MrGroup();
            this.StorePanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator1.Location = new System.Drawing.Point(9, 198);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(365, 2);
            this.clsSeparator1.TabIndex = 78;
            this.clsSeparator1.TabStop = false;
            // 
            // ChkSelectAll
            // 
            this.ChkSelectAll.Checked = true;
            this.ChkSelectAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkSelectAll.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSelectAll.Location = new System.Drawing.Point(9, 107);
            this.ChkSelectAll.Name = "ChkSelectAll";
            this.ChkSelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSelectAll.Size = new System.Drawing.Size(114, 26);
            this.ChkSelectAll.TabIndex = 4;
            this.ChkSelectAll.Text = "Select All";
            this.ChkSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSelectAll.UseVisualStyleBackColor = true;
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.mrGroup1);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(383, 243);
            this.StorePanel.TabIndex = 13;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rChkProductDetails);
            this.groupBox2.Controls.Add(this.rChkVoucherDetails);
            this.groupBox2.Controls.Add(this.rChkOpeningOnly);
            this.groupBox2.Controls.Add(this.ChkSelectAll);
            this.groupBox2.Location = new System.Drawing.Point(216, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(157, 168);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Group By";
            // 
            // rChkProductDetails
            // 
            this.rChkProductDetails.AutoSize = true;
            this.rChkProductDetails.Location = new System.Drawing.Point(10, 53);
            this.rChkProductDetails.Name = "rChkProductDetails";
            this.rChkProductDetails.Size = new System.Drawing.Size(81, 23);
            this.rChkProductDetails.TabIndex = 0;
            this.rChkProductDetails.Text = "Details";
            this.rChkProductDetails.UseVisualStyleBackColor = true;
            // 
            // rChkVoucherDetails
            // 
            this.rChkVoucherDetails.AutoSize = true;
            this.rChkVoucherDetails.Location = new System.Drawing.Point(10, 82);
            this.rChkVoucherDetails.Name = "rChkVoucherDetails";
            this.rChkVoucherDetails.Size = new System.Drawing.Size(136, 23);
            this.rChkVoucherDetails.TabIndex = 1;
            this.rChkVoucherDetails.Text = "Voucher Basic";
            this.rChkVoucherDetails.UseVisualStyleBackColor = true;
            // 
            // rChkOpeningOnly
            // 
            this.rChkOpeningOnly.AutoSize = true;
            this.rChkOpeningOnly.Checked = true;
            this.rChkOpeningOnly.Location = new System.Drawing.Point(9, 24);
            this.rChkOpeningOnly.Name = "rChkOpeningOnly";
            this.rChkOpeningOnly.Size = new System.Drawing.Size(89, 23);
            this.rChkOpeningOnly.TabIndex = 2;
            this.rChkOpeningOnly.TabStop = true;
            this.rChkOpeningOnly.Text = "Opening";
            this.rChkOpeningOnly.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rChkGodownWise);
            this.groupBox1.Controls.Add(this.rChkCompanyWise);
            this.groupBox1.Controls.Add(this.rChkNormal);
            this.groupBox1.Controls.Add(this.rChkSubGroupWise);
            this.groupBox1.Controls.Add(this.rChkGroupWise);
            this.groupBox1.Location = new System.Drawing.Point(10, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 170);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Group By";
            // 
            // rChkGodownWise
            // 
            this.rChkGodownWise.AutoSize = true;
            this.rChkGodownWise.Location = new System.Drawing.Point(19, 140);
            this.rChkGodownWise.Name = "rChkGodownWise";
            this.rChkGodownWise.Size = new System.Drawing.Size(132, 23);
            this.rChkGodownWise.TabIndex = 4;
            this.rChkGodownWise.Text = "Godown Basic";
            this.rChkGodownWise.UseVisualStyleBackColor = true;
            // 
            // rChkCompanyWise
            // 
            this.rChkCompanyWise.AutoSize = true;
            this.rChkCompanyWise.Location = new System.Drawing.Point(19, 111);
            this.rChkCompanyWise.Name = "rChkCompanyWise";
            this.rChkCompanyWise.Size = new System.Drawing.Size(141, 23);
            this.rChkCompanyWise.TabIndex = 3;
            this.rChkCompanyWise.Text = "Company Basic";
            this.rChkCompanyWise.UseVisualStyleBackColor = true;
            // 
            // rChkNormal
            // 
            this.rChkNormal.AutoSize = true;
            this.rChkNormal.Checked = true;
            this.rChkNormal.Location = new System.Drawing.Point(19, 24);
            this.rChkNormal.Name = "rChkNormal";
            this.rChkNormal.Size = new System.Drawing.Size(81, 23);
            this.rChkNormal.TabIndex = 0;
            this.rChkNormal.TabStop = true;
            this.rChkNormal.Text = "Normal";
            this.rChkNormal.UseVisualStyleBackColor = true;
            this.rChkNormal.CheckedChanged += new System.EventHandler(this.RbtnNormal_CheckedChanged);
            // 
            // rChkSubGroupWise
            // 
            this.rChkSubGroupWise.AutoSize = true;
            this.rChkSubGroupWise.Location = new System.Drawing.Point(19, 82);
            this.rChkSubGroupWise.Name = "rChkSubGroupWise";
            this.rChkSubGroupWise.Size = new System.Drawing.Size(146, 23);
            this.rChkSubGroupWise.TabIndex = 2;
            this.rChkSubGroupWise.Text = "SubGroup Basic";
            this.rChkSubGroupWise.UseVisualStyleBackColor = true;
            // 
            // rChkGroupWise
            // 
            this.rChkGroupWise.AutoSize = true;
            this.rChkGroupWise.Location = new System.Drawing.Point(19, 53);
            this.rChkGroupWise.Name = "rChkGroupWise";
            this.rChkGroupWise.Size = new System.Drawing.Size(118, 23);
            this.rChkGroupWise.TabIndex = 1;
            this.rChkGroupWise.Text = "Group Basic";
            this.rChkGroupWise.UseVisualStyleBackColor = true;
            // 
            // btn_Show
            // 
            this.btn_Show.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Show.Appearance.Options.UseFont = true;
            this.btn_Show.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.btn_Show.Location = new System.Drawing.Point(102, 202);
            this.btn_Show.Name = "btn_Show";
            this.btn_Show.Size = new System.Drawing.Size(96, 37);
            this.btn_Show.TabIndex = 5;
            this.btn_Show.Text = "&SHOW";
            this.btn_Show.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(200, 202);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 37);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&CANCEL";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.groupBox1);
            this.mrGroup1.Controls.Add(this.clsSeparator1);
            this.mrGroup1.Controls.Add(this.groupBox2);
            this.mrGroup1.Controls.Add(this.btn_Show);
            this.mrGroup1.Controls.Add(this.btnCancel);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "";
            this.mrGroup1.Location = new System.Drawing.Point(0, 0);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 10;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(383, 243);
            this.mrGroup1.TabIndex = 79;
            // 
            // FrmProductOpeningReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 243);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmProductOpeningReport";
            this.ShowIcon = false;
            this.Text = "Product Opening";
            this.Load += new System.EventHandler(this.FrmProductOpening_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmProductOpening_KeyPress);
            this.StorePanel.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.mrGroup1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ClsSeparator clsSeparator1;
        private DevExpress.XtraEditors.SimpleButton btn_Show;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.CheckBox ChkSelectAll;
        private System.Windows.Forms.RadioButton rChkNormal;
        private System.Windows.Forms.RadioButton rChkSubGroupWise;
        private System.Windows.Forms.RadioButton rChkGroupWise;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rChkProductDetails;
        private System.Windows.Forms.RadioButton rChkVoucherDetails;
        private System.Windows.Forms.RadioButton rChkOpeningOnly;
        private System.Windows.Forms.RadioButton rChkCompanyWise;
        private System.Windows.Forms.RadioButton rChkGodownWise;
        private MrGroup mrGroup1;
        private MrPanel StorePanel;
    }
}