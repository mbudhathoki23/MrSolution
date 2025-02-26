using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.OutStanding_Report
{
    partial class FrmOutstandingPartyLedger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOutstandingPartyLedger));
            this.rChkVendor = new System.Windows.Forms.RadioButton();
            this.rChkCustomer = new System.Windows.Forms.RadioButton();
            this.ChkDate = new System.Windows.Forms.CheckBox();
            this.ChkSelectAll = new System.Windows.Forms.CheckBox();
            this.ChkIncludeAdjustment = new System.Windows.Forms.CheckBox();
            this.ChkBillWiseAgent = new System.Windows.Forms.CheckBox();
            this.MskToDate = new MrMaskedTextBox();
            this.MskFrom = new MrMaskedTextBox();
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.PanelHeader = new MrPanel();
            this.mrGroup3 = new MrGroup();
            this.ChkIncludePDC = new System.Windows.Forms.CheckBox();
            this.clsSeparator3 = new ClsSeparator();
            this.mrGroup2 = new MrGroup();
            this.rChkArea = new System.Windows.Forms.RadioButton();
            this.rChkLedger = new System.Windows.Forms.RadioButton();
            this.rChkAgent = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rChkOpeningOnly = new System.Windows.Forms.RadioButton();
            this.rChkOpeningExclude = new System.Windows.Forms.RadioButton();
            this.rChkOpeningInclude = new System.Windows.Forms.RadioButton();
            this.mrGroup1 = new MrGroup();
            this.CmbDateType = new System.Windows.Forms.ComboBox();
            this.PanelHeader.SuspendLayout();
            this.mrGroup3.SuspendLayout();
            this.mrGroup2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rChkVendor
            // 
            this.rChkVendor.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rChkVendor.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChkVendor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rChkVendor.Location = new System.Drawing.Point(126, 27);
            this.rChkVendor.Name = "rChkVendor";
            this.rChkVendor.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rChkVendor.Size = new System.Drawing.Size(119, 24);
            this.rChkVendor.TabIndex = 1;
            this.rChkVendor.Text = "Vendor";
            this.rChkVendor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rChkVendor.UseVisualStyleBackColor = true;
            // 
            // rChkCustomer
            // 
            this.rChkCustomer.Checked = true;
            this.rChkCustomer.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rChkCustomer.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChkCustomer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rChkCustomer.Location = new System.Drawing.Point(5, 28);
            this.rChkCustomer.Name = "rChkCustomer";
            this.rChkCustomer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rChkCustomer.Size = new System.Drawing.Size(113, 22);
            this.rChkCustomer.TabIndex = 0;
            this.rChkCustomer.TabStop = true;
            this.rChkCustomer.Text = "Customer";
            this.rChkCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rChkCustomer.UseVisualStyleBackColor = true;
            // 
            // ChkDate
            // 
            this.ChkDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDate.Location = new System.Drawing.Point(182, 27);
            this.ChkDate.Name = "ChkDate";
            this.ChkDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDate.Size = new System.Drawing.Size(104, 24);
            this.ChkDate.TabIndex = 2;
            this.ChkDate.Text = "Date";
            this.ChkDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDate.UseVisualStyleBackColor = true;
            // 
            // ChkSelectAll
            // 
            this.ChkSelectAll.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSelectAll.Location = new System.Drawing.Point(182, 50);
            this.ChkSelectAll.Name = "ChkSelectAll";
            this.ChkSelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSelectAll.Size = new System.Drawing.Size(104, 24);
            this.ChkSelectAll.TabIndex = 4;
            this.ChkSelectAll.Text = "Select All";
            this.ChkSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSelectAll.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeAdjustment
            // 
            this.ChkIncludeAdjustment.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeAdjustment.Location = new System.Drawing.Point(14, 27);
            this.ChkIncludeAdjustment.Name = "ChkIncludeAdjustment";
            this.ChkIncludeAdjustment.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeAdjustment.Size = new System.Drawing.Size(162, 24);
            this.ChkIncludeAdjustment.TabIndex = 0;
            this.ChkIncludeAdjustment.Text = "With Adjustment";
            this.ChkIncludeAdjustment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeAdjustment.UseVisualStyleBackColor = true;
            // 
            // ChkBillWiseAgent
            // 
            this.ChkBillWiseAgent.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkBillWiseAgent.Location = new System.Drawing.Point(14, 50);
            this.ChkBillWiseAgent.Name = "ChkBillWiseAgent";
            this.ChkBillWiseAgent.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkBillWiseAgent.Size = new System.Drawing.Size(162, 24);
            this.ChkBillWiseAgent.TabIndex = 1;
            this.ChkBillWiseAgent.Text = "Bill Agent";
            this.ChkBillWiseAgent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkBillWiseAgent.UseVisualStyleBackColor = true;
            // 
            // MskToDate
            // 
            this.MskToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskToDate.Location = new System.Drawing.Point(350, 51);
            this.MskToDate.Mask = "00/00/0000";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MskToDate.Size = new System.Drawing.Size(114, 26);
            this.MskToDate.TabIndex = 4;
            this.MskToDate.Validated += new System.EventHandler(this.MskToDate_Validated);
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskFrom.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskFrom.Location = new System.Drawing.Point(234, 51);
            this.MskFrom.Mask = "00/00/0000";
            this.MskFrom.Name = "MskFrom";
            this.MskFrom.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MskFrom.Size = new System.Drawing.Size(114, 26);
            this.MskFrom.TabIndex = 3;
            this.MskFrom.Validated += new System.EventHandler(this.MskFrom_Validated);
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.Location = new System.Drawing.Point(248, 83);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(99, 35);
            this.BtnShow.TabIndex = 5;
            this.BtnShow.Text = "&SHOW";
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.Location = new System.Drawing.Point(348, 83);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(110, 35);
            this.BtnCancel.TabIndex = 6;
            this.BtnCancel.Text = "&CANCEL";
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.mrGroup3);
            this.PanelHeader.Controls.Add(this.mrGroup2);
            this.PanelHeader.Controls.Add(this.mrGroup1);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(473, 308);
            this.PanelHeader.TabIndex = 0;
            // 
            // mrGroup3
            // 
            this.mrGroup3.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup3.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup3.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup3.BorderColor = System.Drawing.Color.White;
            this.mrGroup3.BorderThickness = 1F;
            this.mrGroup3.Controls.Add(this.ChkIncludePDC);
            this.mrGroup3.Controls.Add(this.BtnShow);
            this.mrGroup3.Controls.Add(this.ChkDate);
            this.mrGroup3.Controls.Add(this.BtnCancel);
            this.mrGroup3.Controls.Add(this.ChkSelectAll);
            this.mrGroup3.Controls.Add(this.ChkIncludeAdjustment);
            this.mrGroup3.Controls.Add(this.ChkBillWiseAgent);
            this.mrGroup3.Controls.Add(this.clsSeparator3);
            this.mrGroup3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup3.GroupImage = null;
            this.mrGroup3.GroupTitle = "Filter Value";
            this.mrGroup3.Location = new System.Drawing.Point(3, 186);
            this.mrGroup3.Name = "mrGroup3";
            this.mrGroup3.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup3.PaintGroupBox = false;
            this.mrGroup3.RoundCorners = 4;
            this.mrGroup3.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup3.ShadowControl = false;
            this.mrGroup3.ShadowThickness = 3;
            this.mrGroup3.Size = new System.Drawing.Size(468, 120);
            this.mrGroup3.TabIndex = 2;
            // 
            // ChkIncludePDC
            // 
            this.ChkIncludePDC.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludePDC.Location = new System.Drawing.Point(292, 27);
            this.ChkIncludePDC.Name = "ChkIncludePDC";
            this.ChkIncludePDC.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludePDC.Size = new System.Drawing.Size(134, 24);
            this.ChkIncludePDC.TabIndex = 3;
            this.ChkIncludePDC.Text = "Include PDC";
            this.ChkIncludePDC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludePDC.UseVisualStyleBackColor = true;
            // 
            // clsSeparator3
            // 
            this.clsSeparator3.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator3.Location = new System.Drawing.Point(11, 80);
            this.clsSeparator3.Name = "clsSeparator3";
            this.clsSeparator3.Size = new System.Drawing.Size(465, 2);
            this.clsSeparator3.TabIndex = 71;
            this.clsSeparator3.TabStop = false;
            // 
            // mrGroup2
            // 
            this.mrGroup2.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup2.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup2.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup2.BorderColor = System.Drawing.Color.White;
            this.mrGroup2.BorderThickness = 1F;
            this.mrGroup2.Controls.Add(this.rChkArea);
            this.mrGroup2.Controls.Add(this.rChkLedger);
            this.mrGroup2.Controls.Add(this.rChkAgent);
            this.mrGroup2.Controls.Add(this.groupBox2);
            this.mrGroup2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup2.GroupImage = null;
            this.mrGroup2.GroupTitle = "Report Type";
            this.mrGroup2.Location = new System.Drawing.Point(3, 84);
            this.mrGroup2.Name = "mrGroup2";
            this.mrGroup2.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup2.PaintGroupBox = false;
            this.mrGroup2.RoundCorners = 10;
            this.mrGroup2.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup2.ShadowControl = false;
            this.mrGroup2.ShadowThickness = 3;
            this.mrGroup2.Size = new System.Drawing.Size(468, 101);
            this.mrGroup2.TabIndex = 1;
            // 
            // rChkArea
            // 
            this.rChkArea.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rChkArea.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChkArea.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rChkArea.Location = new System.Drawing.Point(5, 72);
            this.rChkArea.Name = "rChkArea";
            this.rChkArea.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rChkArea.Size = new System.Drawing.Size(127, 22);
            this.rChkArea.TabIndex = 2;
            this.rChkArea.Text = "Area";
            this.rChkArea.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rChkArea.UseVisualStyleBackColor = true;
            // 
            // rChkLedger
            // 
            this.rChkLedger.Checked = true;
            this.rChkLedger.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rChkLedger.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChkLedger.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rChkLedger.Location = new System.Drawing.Point(5, 28);
            this.rChkLedger.Name = "rChkLedger";
            this.rChkLedger.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rChkLedger.Size = new System.Drawing.Size(127, 22);
            this.rChkLedger.TabIndex = 0;
            this.rChkLedger.TabStop = true;
            this.rChkLedger.Text = "Ledger";
            this.rChkLedger.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rChkLedger.UseVisualStyleBackColor = true;
            // 
            // rChkAgent
            // 
            this.rChkAgent.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rChkAgent.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChkAgent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rChkAgent.Location = new System.Drawing.Point(5, 50);
            this.rChkAgent.Name = "rChkAgent";
            this.rChkAgent.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rChkAgent.Size = new System.Drawing.Size(127, 22);
            this.rChkAgent.TabIndex = 1;
            this.rChkAgent.Text = "Agent";
            this.rChkAgent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rChkAgent.UseVisualStyleBackColor = true;
            this.rChkAgent.CheckedChanged += new System.EventHandler(this.rChkAgent_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rChkOpeningOnly);
            this.groupBox2.Controls.Add(this.rChkOpeningExclude);
            this.groupBox2.Controls.Add(this.rChkOpeningInclude);
            this.groupBox2.Location = new System.Drawing.Point(300, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox2.Size = new System.Drawing.Size(164, 84);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Opening On";
            // 
            // rChkOpeningOnly
            // 
            this.rChkOpeningOnly.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rChkOpeningOnly.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChkOpeningOnly.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rChkOpeningOnly.Location = new System.Drawing.Point(6, 62);
            this.rChkOpeningOnly.Name = "rChkOpeningOnly";
            this.rChkOpeningOnly.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rChkOpeningOnly.Size = new System.Drawing.Size(113, 22);
            this.rChkOpeningOnly.TabIndex = 2;
            this.rChkOpeningOnly.Text = "Only";
            this.rChkOpeningOnly.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rChkOpeningOnly.UseVisualStyleBackColor = true;
            // 
            // rChkOpeningExclude
            // 
            this.rChkOpeningExclude.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rChkOpeningExclude.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChkOpeningExclude.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rChkOpeningExclude.Location = new System.Drawing.Point(6, 40);
            this.rChkOpeningExclude.Name = "rChkOpeningExclude";
            this.rChkOpeningExclude.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rChkOpeningExclude.Size = new System.Drawing.Size(113, 22);
            this.rChkOpeningExclude.TabIndex = 1;
            this.rChkOpeningExclude.Text = "Exclude";
            this.rChkOpeningExclude.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rChkOpeningExclude.UseVisualStyleBackColor = true;
            // 
            // rChkOpeningInclude
            // 
            this.rChkOpeningInclude.Checked = true;
            this.rChkOpeningInclude.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rChkOpeningInclude.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChkOpeningInclude.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rChkOpeningInclude.Location = new System.Drawing.Point(6, 18);
            this.rChkOpeningInclude.Name = "rChkOpeningInclude";
            this.rChkOpeningInclude.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rChkOpeningInclude.Size = new System.Drawing.Size(113, 22);
            this.rChkOpeningInclude.TabIndex = 0;
            this.rChkOpeningInclude.TabStop = true;
            this.rChkOpeningInclude.Text = "Include";
            this.rChkOpeningInclude.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rChkOpeningInclude.UseVisualStyleBackColor = true;
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.CmbDateType);
            this.mrGroup1.Controls.Add(this.MskFrom);
            this.mrGroup1.Controls.Add(this.rChkCustomer);
            this.mrGroup1.Controls.Add(this.MskToDate);
            this.mrGroup1.Controls.Add(this.rChkVendor);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "Date Filter";
            this.mrGroup1.Location = new System.Drawing.Point(3, 2);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 4;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(468, 81);
            this.mrGroup1.TabIndex = 0;
            // 
            // CmbDateType
            // 
            this.CmbDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDateType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbDateType.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbDateType.FormattingEnabled = true;
            this.CmbDateType.Items.AddRange(new object[] {
            "Custom Date",
            "Today",
            "Yesterday",
            "Current Week",
            "Last Week",
            "Current Month",
            "Last Month",
            "Upto Date",
            "Accounting Period"});
            this.CmbDateType.Location = new System.Drawing.Point(5, 50);
            this.CmbDateType.Name = "CmbDateType";
            this.CmbDateType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CmbDateType.Size = new System.Drawing.Size(223, 28);
            this.CmbDateType.TabIndex = 2;
            this.CmbDateType.SelectedIndexChanged += new System.EventHandler(this.CmbDateType_SelectedIndexChanged);
            // 
            // FrmOutstandingPartyLedger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(473, 308);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmOutstandingPartyLedger";
            this.Text = "OUTSTANDING PARTY LEDGER";
            this.Load += new System.EventHandler(this.OutstandingPartyLedger_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OutstandingPartyLedger_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            this.mrGroup3.ResumeLayout(false);
            this.mrGroup2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox ChkDate;
        private System.Windows.Forms.CheckBox ChkSelectAll;
        private System.Windows.Forms.CheckBox ChkIncludeAdjustment;
        private System.Windows.Forms.CheckBox ChkBillWiseAgent;
        private System.Windows.Forms.RadioButton rChkVendor;
        private System.Windows.Forms.RadioButton rChkCustomer;
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private ClsSeparator clsSeparator3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rChkLedger;
        private System.Windows.Forms.RadioButton rChkArea;
        private System.Windows.Forms.RadioButton rChkAgent;
        private System.Windows.Forms.RadioButton rChkOpeningExclude;
        private System.Windows.Forms.RadioButton rChkOpeningInclude;
        private System.Windows.Forms.RadioButton rChkOpeningOnly;
        private System.Windows.Forms.ComboBox CmbDateType;
        private System.Windows.Forms.CheckBox ChkIncludePDC;
        private MrGroup mrGroup1;
        private MrGroup mrGroup2;
        private MrGroup mrGroup3;
        private MrMaskedTextBox MskToDate;
        private MrMaskedTextBox MskFrom;
        private MrPanel PanelHeader;
    }
}