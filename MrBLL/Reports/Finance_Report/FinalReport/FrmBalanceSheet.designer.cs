using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MrDAL.Control.ControlsEx.Control;
using ComboBox = System.Windows.Forms.ComboBox;

namespace MrBLL.Reports.Finance_Report.FinalReport
{
    partial class FrmBalanceSheet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.PanelHeader = new MrPanel();
            this.mrGroup3 = new MrGroup();
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator2 = new ClsSeparator();
            this.ChkIncluePDC = new System.Windows.Forms.CheckBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.ChkIncludeLedger = new System.Windows.Forms.CheckBox();
            this.ChkAdvance = new System.Windows.Forms.CheckBox();
            this.ChkShortName = new System.Windows.Forms.CheckBox();
            this.ChkIncludeClosingStock = new System.Windows.Forms.CheckBox();
            this.ChkCombineCustomerVendor = new System.Windows.Forms.CheckBox();
            this.ChkTFormat = new System.Windows.Forms.CheckBox();
            this.ChkIncludeSubledger = new System.Windows.Forms.CheckBox();
            this.ChkDate = new System.Windows.Forms.CheckBox();
            this.ChkZeroBalance = new System.Windows.Forms.CheckBox();
            this.ChkRePost = new System.Windows.Forms.CheckBox();
            this.mrGroup2 = new MrGroup();
            this.rChkAccountSubGroup = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rChkSchedule = new System.Windows.Forms.RadioButton();
            this.rChkDescription = new System.Windows.Forms.RadioButton();
            this.rChkShortName = new System.Windows.Forms.RadioButton();
            this.rChkLedger = new System.Windows.Forms.RadioButton();
            this.rChkAccountGroup = new System.Windows.Forms.RadioButton();
            this.mrGroup1 = new MrGroup();
            this.MskToDate = new MrMaskedTextBox();
            this.rChkOpening = new System.Windows.Forms.RadioButton();
            this.MskFrom = new MrMaskedTextBox();
            this.CmbDateType = new System.Windows.Forms.ComboBox();
            this.rChkPeriodic = new System.Windows.Forms.RadioButton();
            this.rChkNormal = new System.Windows.Forms.RadioButton();
            this.mrGroup4 = new MrGroup();
            this.PanelHeader.SuspendLayout();
            this.mrGroup3.SuspendLayout();
            this.mrGroup2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            this.mrGroup4.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.mrGroup4);
            this.PanelHeader.Controls.Add(this.mrGroup3);
            this.PanelHeader.Controls.Add(this.mrGroup2);
            this.PanelHeader.Controls.Add(this.mrGroup1);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(502, 381);
            this.PanelHeader.TabIndex = 0;
            // 
            // mrGroup3
            // 
            this.mrGroup3.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup3.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup3.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup3.BorderColor = System.Drawing.Color.White;
            this.mrGroup3.BorderThickness = 1F;
            this.mrGroup3.Controls.Add(this.BtnShow);
            this.mrGroup3.Controls.Add(this.clsSeparator2);
            this.mrGroup3.Controls.Add(this.ChkIncluePDC);
            this.mrGroup3.Controls.Add(this.BtnCancel);
            this.mrGroup3.Controls.Add(this.ChkIncludeLedger);
            this.mrGroup3.Controls.Add(this.ChkAdvance);
            this.mrGroup3.Controls.Add(this.ChkShortName);
            this.mrGroup3.Controls.Add(this.ChkIncludeClosingStock);
            this.mrGroup3.Controls.Add(this.ChkCombineCustomerVendor);
            this.mrGroup3.Controls.Add(this.ChkTFormat);
            this.mrGroup3.Controls.Add(this.ChkIncludeSubledger);
            this.mrGroup3.Controls.Add(this.ChkDate);
            this.mrGroup3.Controls.Add(this.ChkZeroBalance);
            this.mrGroup3.Controls.Add(this.ChkRePost);
            this.mrGroup3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup3.GroupImage = null;
            this.mrGroup3.GroupTitle = "Filter Report";
            this.mrGroup3.Location = new System.Drawing.Point(3, 214);
            this.mrGroup3.Name = "mrGroup3";
            this.mrGroup3.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup3.PaintGroupBox = false;
            this.mrGroup3.RoundCorners = 10;
            this.mrGroup3.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup3.ShadowControl = true;
            this.mrGroup3.ShadowThickness = 3;
            this.mrGroup3.Size = new System.Drawing.Size(500, 166);
            this.mrGroup3.TabIndex = 2;
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnShow.Location = new System.Drawing.Point(154, 120);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(102, 38);
            this.BtnShow.TabIndex = 11;
            this.BtnShow.Text = "&SHOW";
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator2.Location = new System.Drawing.Point(6, 117);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(486, 2);
            this.clsSeparator2.TabIndex = 103;
            this.clsSeparator2.TabStop = false;
            // 
            // ChkIncluePDC
            // 
            this.ChkIncluePDC.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncluePDC.Location = new System.Drawing.Point(184, 26);
            this.ChkIncluePDC.Name = "ChkIncluePDC";
            this.ChkIncluePDC.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncluePDC.Size = new System.Drawing.Size(146, 23);
            this.ChkIncluePDC.TabIndex = 4;
            this.ChkIncluePDC.Text = "Include PDC";
            this.ChkIncluePDC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncluePDC.UseVisualStyleBackColor = true;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(256, 120);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(112, 38);
            this.BtnCancel.TabIndex = 12;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // ChkIncludeLedger
            // 
            this.ChkIncludeLedger.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeLedger.Location = new System.Drawing.Point(6, 26);
            this.ChkIncludeLedger.Name = "ChkIncludeLedger";
            this.ChkIncludeLedger.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeLedger.Size = new System.Drawing.Size(173, 23);
            this.ChkIncludeLedger.TabIndex = 0;
            this.ChkIncludeLedger.Text = "Include Ledger";
            this.ChkIncludeLedger.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeLedger.UseVisualStyleBackColor = true;
            // 
            // ChkAdvance
            // 
            this.ChkAdvance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAdvance.Location = new System.Drawing.Point(184, 72);
            this.ChkAdvance.Name = "ChkAdvance";
            this.ChkAdvance.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkAdvance.Size = new System.Drawing.Size(146, 23);
            this.ChkAdvance.TabIndex = 6;
            this.ChkAdvance.Text = "Advance";
            this.ChkAdvance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkAdvance.UseVisualStyleBackColor = true;
            // 
            // ChkShortName
            // 
            this.ChkShortName.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkShortName.Location = new System.Drawing.Point(6, 72);
            this.ChkShortName.Name = "ChkShortName";
            this.ChkShortName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkShortName.Size = new System.Drawing.Size(173, 23);
            this.ChkShortName.TabIndex = 2;
            this.ChkShortName.Text = "ShortName";
            this.ChkShortName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkShortName.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeClosingStock
            // 
            this.ChkIncludeClosingStock.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeClosingStock.Location = new System.Drawing.Point(336, 26);
            this.ChkIncludeClosingStock.Name = "ChkIncludeClosingStock";
            this.ChkIncludeClosingStock.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeClosingStock.Size = new System.Drawing.Size(156, 24);
            this.ChkIncludeClosingStock.TabIndex = 8;
            this.ChkIncludeClosingStock.Text = "Closing Stock";
            this.ChkIncludeClosingStock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeClosingStock.UseVisualStyleBackColor = true;
            this.ChkIncludeClosingStock.CheckedChanged += new System.EventHandler(this.ChkIncludeClosingStock_CheckedChanged);
            // 
            // ChkCombineCustomerVendor
            // 
            this.ChkCombineCustomerVendor.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkCombineCustomerVendor.Location = new System.Drawing.Point(336, 50);
            this.ChkCombineCustomerVendor.Name = "ChkCombineCustomerVendor";
            this.ChkCombineCustomerVendor.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkCombineCustomerVendor.Size = new System.Drawing.Size(156, 24);
            this.ChkCombineCustomerVendor.TabIndex = 9;
            this.ChkCombineCustomerVendor.Text = "Combine C/V";
            this.ChkCombineCustomerVendor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCombineCustomerVendor.UseVisualStyleBackColor = true;
            // 
            // ChkTFormat
            // 
            this.ChkTFormat.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkTFormat.Location = new System.Drawing.Point(6, 95);
            this.ChkTFormat.Name = "ChkTFormat";
            this.ChkTFormat.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkTFormat.Size = new System.Drawing.Size(173, 23);
            this.ChkTFormat.TabIndex = 3;
            this.ChkTFormat.Text = "T Format";
            this.ChkTFormat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkTFormat.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeSubledger
            // 
            this.ChkIncludeSubledger.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeSubledger.Location = new System.Drawing.Point(6, 49);
            this.ChkIncludeSubledger.Name = "ChkIncludeSubledger";
            this.ChkIncludeSubledger.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeSubledger.Size = new System.Drawing.Size(173, 23);
            this.ChkIncludeSubledger.TabIndex = 1;
            this.ChkIncludeSubledger.Text = "Include SubLedger";
            this.ChkIncludeSubledger.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeSubledger.UseVisualStyleBackColor = true;
            // 
            // IsDate
            // 
            this.ChkDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDate.Location = new System.Drawing.Point(336, 74);
            this.ChkDate.Name = "ChkDate";
            this.ChkDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDate.Size = new System.Drawing.Size(156, 24);
            this.ChkDate.TabIndex = 10;
            this.ChkDate.Text = "Date";
            this.ChkDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDate.UseVisualStyleBackColor = true;
            // 
            // ChkZeroBalance
            // 
            this.ChkZeroBalance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkZeroBalance.Location = new System.Drawing.Point(184, 49);
            this.ChkZeroBalance.Name = "ChkZeroBalance";
            this.ChkZeroBalance.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkZeroBalance.Size = new System.Drawing.Size(146, 23);
            this.ChkZeroBalance.TabIndex = 5;
            this.ChkZeroBalance.Text = "Zero Balance";
            this.ChkZeroBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkZeroBalance.UseVisualStyleBackColor = true;
            // 
            // ChkRePost
            // 
            this.ChkRePost.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkRePost.Location = new System.Drawing.Point(184, 95);
            this.ChkRePost.Name = "ChkRePost";
            this.ChkRePost.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkRePost.Size = new System.Drawing.Size(146, 23);
            this.ChkRePost.TabIndex = 7;
            this.ChkRePost.Text = "Re-Post Value";
            this.ChkRePost.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkRePost.UseVisualStyleBackColor = true;
            // 
            // mrGroup2
            // 
            this.mrGroup2.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup2.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup2.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup2.BorderColor = System.Drawing.Color.White;
            this.mrGroup2.BorderThickness = 1F;
            this.mrGroup2.Controls.Add(this.rChkAccountSubGroup);
            this.mrGroup2.Controls.Add(this.groupBox4);
            this.mrGroup2.Controls.Add(this.rChkLedger);
            this.mrGroup2.Controls.Add(this.rChkAccountGroup);
            this.mrGroup2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup2.GroupImage = null;
            this.mrGroup2.GroupTitle = "Report Type";
            this.mrGroup2.Location = new System.Drawing.Point(3, 105);
            this.mrGroup2.Name = "mrGroup2";
            this.mrGroup2.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup2.PaintGroupBox = false;
            this.mrGroup2.RoundCorners = 10;
            this.mrGroup2.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup2.ShadowControl = true;
            this.mrGroup2.ShadowThickness = 3;
            this.mrGroup2.Size = new System.Drawing.Size(500, 110);
            this.mrGroup2.TabIndex = 1;
            // 
            // rChkAccountSubGroup
            // 
            this.rChkAccountSubGroup.AutoSize = true;
            this.rChkAccountSubGroup.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.rChkAccountSubGroup.Location = new System.Drawing.Point(9, 75);
            this.rChkAccountSubGroup.Margin = new System.Windows.Forms.Padding(4);
            this.rChkAccountSubGroup.Name = "rChkAccountSubGroup";
            this.rChkAccountSubGroup.Size = new System.Drawing.Size(178, 24);
            this.rChkAccountSubGroup.TabIndex = 2;
            this.rChkAccountSubGroup.Text = "Account SubGroup";
            this.rChkAccountSubGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rChkAccountSubGroup.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rChkSchedule);
            this.groupBox4.Controls.Add(this.rChkDescription);
            this.groupBox4.Controls.Add(this.rChkShortName);
            this.groupBox4.Location = new System.Drawing.Point(217, 11);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(150, 90);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Sort On";
            // 
            // rChkSchedule
            // 
            this.rChkSchedule.AutoSize = true;
            this.rChkSchedule.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.rChkSchedule.Location = new System.Drawing.Point(7, 63);
            this.rChkSchedule.Margin = new System.Windows.Forms.Padding(4);
            this.rChkSchedule.Name = "rChkSchedule";
            this.rChkSchedule.Size = new System.Drawing.Size(101, 24);
            this.rChkSchedule.TabIndex = 2;
            this.rChkSchedule.Text = "Schedule";
            this.rChkSchedule.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rChkSchedule.UseVisualStyleBackColor = true;
            // 
            // rChkDescription
            // 
            this.rChkDescription.AutoSize = true;
            this.rChkDescription.Checked = true;
            this.rChkDescription.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.rChkDescription.Location = new System.Drawing.Point(7, 15);
            this.rChkDescription.Margin = new System.Windows.Forms.Padding(4);
            this.rChkDescription.Name = "rChkDescription";
            this.rChkDescription.Size = new System.Drawing.Size(118, 24);
            this.rChkDescription.TabIndex = 0;
            this.rChkDescription.TabStop = true;
            this.rChkDescription.Text = "Description";
            this.rChkDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rChkDescription.UseVisualStyleBackColor = true;
            // 
            // rChkShortName
            // 
            this.rChkShortName.AutoSize = true;
            this.rChkShortName.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.rChkShortName.Location = new System.Drawing.Point(7, 39);
            this.rChkShortName.Margin = new System.Windows.Forms.Padding(4);
            this.rChkShortName.Name = "rChkShortName";
            this.rChkShortName.Size = new System.Drawing.Size(116, 24);
            this.rChkShortName.TabIndex = 1;
            this.rChkShortName.Text = "ShortName";
            this.rChkShortName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rChkLedger
            // 
            this.rChkLedger.AutoSize = true;
            this.rChkLedger.Checked = true;
            this.rChkLedger.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.rChkLedger.Location = new System.Drawing.Point(9, 27);
            this.rChkLedger.Margin = new System.Windows.Forms.Padding(4);
            this.rChkLedger.Name = "rChkLedger";
            this.rChkLedger.Size = new System.Drawing.Size(81, 24);
            this.rChkLedger.TabIndex = 0;
            this.rChkLedger.TabStop = true;
            this.rChkLedger.Text = "Ledger";
            this.rChkLedger.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rChkLedger.UseVisualStyleBackColor = true;
            this.rChkLedger.CheckedChanged += new System.EventHandler(this.rChkLedger_CheckedChanged);
            // 
            // rChkAccountGroup
            // 
            this.rChkAccountGroup.AutoSize = true;
            this.rChkAccountGroup.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.rChkAccountGroup.Location = new System.Drawing.Point(9, 51);
            this.rChkAccountGroup.Margin = new System.Windows.Forms.Padding(4);
            this.rChkAccountGroup.Name = "rChkAccountGroup";
            this.rChkAccountGroup.Size = new System.Drawing.Size(146, 24);
            this.rChkAccountGroup.TabIndex = 1;
            this.rChkAccountGroup.Text = "Account Group";
            this.rChkAccountGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rChkAccountGroup.UseVisualStyleBackColor = true;
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.MskToDate);
            this.mrGroup1.Controls.Add(this.MskFrom);
            this.mrGroup1.Controls.Add(this.CmbDateType);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "Date Filter";
            this.mrGroup1.Location = new System.Drawing.Point(3, 41);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 10;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = true;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(500, 64);
            this.mrGroup1.TabIndex = 0;
            // 
            // MskToDate
            // 
            this.MskToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskToDate.Location = new System.Drawing.Point(367, 28);
            this.MskToDate.Mask = "00/00/0000";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.Size = new System.Drawing.Size(125, 26);
            this.MskToDate.TabIndex = 5;
            this.MskToDate.Enter += new System.EventHandler(this.MskToDate_Enter);
            this.MskToDate.Leave += new System.EventHandler(this.MskToDate_Leave);
            // 
            // rChkOpening
            // 
            this.rChkOpening.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChkOpening.Location = new System.Drawing.Point(206, 18);
            this.rChkOpening.Name = "rChkOpening";
            this.rChkOpening.Size = new System.Drawing.Size(96, 24);
            this.rChkOpening.TabIndex = 2;
            this.rChkOpening.Text = "Opening";
            this.rChkOpening.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rChkOpening.UseVisualStyleBackColor = true;
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskFrom.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskFrom.Location = new System.Drawing.Point(240, 28);
            this.MskFrom.Mask = "00/00/0000";
            this.MskFrom.Name = "MskFrom";
            this.MskFrom.Size = new System.Drawing.Size(124, 26);
            this.MskFrom.TabIndex = 4;
            this.MskFrom.Enter += new System.EventHandler(this.MskFrom_Enter);
            this.MskFrom.Leave += new System.EventHandler(this.MskFrom_Leave);
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
            this.CmbDateType.Location = new System.Drawing.Point(3, 27);
            this.CmbDateType.Name = "CmbDateType";
            this.CmbDateType.Size = new System.Drawing.Size(235, 28);
            this.CmbDateType.TabIndex = 3;
            this.CmbDateType.SelectedIndexChanged += new System.EventHandler(this.CmbDateType_SelectedIndexChanged);
            // 
            // rChkPeriodic
            // 
            this.rChkPeriodic.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChkPeriodic.Location = new System.Drawing.Point(110, 18);
            this.rChkPeriodic.Name = "rChkPeriodic";
            this.rChkPeriodic.Size = new System.Drawing.Size(96, 24);
            this.rChkPeriodic.TabIndex = 1;
            this.rChkPeriodic.Text = "Periodic";
            this.rChkPeriodic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rChkPeriodic.UseVisualStyleBackColor = true;
            this.rChkPeriodic.CheckedChanged += new System.EventHandler(this.rChkPeriodic_CheckedChanged);
            // 
            // rChkNormal
            // 
            this.rChkNormal.Checked = true;
            this.rChkNormal.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChkNormal.Location = new System.Drawing.Point(14, 18);
            this.rChkNormal.Name = "rChkNormal";
            this.rChkNormal.Size = new System.Drawing.Size(96, 24);
            this.rChkNormal.TabIndex = 0;
            this.rChkNormal.TabStop = true;
            this.rChkNormal.Text = "Normal ";
            this.rChkNormal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rChkNormal.UseVisualStyleBackColor = true;
            this.rChkNormal.CheckedChanged += new System.EventHandler(this.rChkNormal_CheckedChanged);
            // 
            // mrGroup4
            // 
            this.mrGroup4.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup4.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup4.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup4.BorderColor = System.Drawing.Color.White;
            this.mrGroup4.BorderThickness = 1F;
            this.mrGroup4.Controls.Add(this.rChkNormal);
            this.mrGroup4.Controls.Add(this.rChkOpening);
            this.mrGroup4.Controls.Add(this.rChkPeriodic);
            this.mrGroup4.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup4.GroupImage = null;
            this.mrGroup4.GroupTitle = "";
            this.mrGroup4.Location = new System.Drawing.Point(3, -10);
            this.mrGroup4.Name = "mrGroup4";
            this.mrGroup4.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup4.PaintGroupBox = false;
            this.mrGroup4.RoundCorners = 10;
            this.mrGroup4.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup4.ShadowControl = true;
            this.mrGroup4.ShadowThickness = 3;
            this.mrGroup4.Size = new System.Drawing.Size(500, 51);
            this.mrGroup4.TabIndex = 3;
            // 
            // FrmBalanceSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(502, 381);
            this.Controls.Add(this.PanelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmBalanceSheet";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BALANCE SHEET REPORT";
            this.Load += new System.EventHandler(this.FrmBalanceSheet_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmBalanceSheet_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmBalanceSheet_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            this.mrGroup3.ResumeLayout(false);
            this.mrGroup2.ResumeLayout(false);
            this.mrGroup2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.mrGroup4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private RadioButton rChkPeriodic;
        private RadioButton rChkNormal;
        private CheckBox ChkIncluePDC;
        private CheckBox ChkTFormat;
        private CheckBox ChkDate;
        private CheckBox ChkIncludeClosingStock;
        private ComboBox CmbDateType;
        private CheckBox ChkZeroBalance;
        private ClsSeparator clsSeparator2;
        private CheckBox ChkAdvance;
        private CheckBox ChkCombineCustomerVendor;
        private SimpleButton BtnShow;
        private SimpleButton BtnCancel;
        private RadioButton rChkLedger;
        private RadioButton rChkAccountSubGroup;
        private RadioButton rChkAccountGroup;
		private CheckBox ChkShortName;
		private CheckBox ChkIncludeLedger;
		private CheckBox ChkIncludeSubledger;
		private RadioButton rChkDescription;
		private RadioButton rChkSchedule;
		private RadioButton rChkShortName;
        private RadioButton rChkOpening;
        private GroupBox groupBox4;
        private MrGroup mrGroup1;
        private MrGroup mrGroup2;
        private MrGroup mrGroup3;
        private CheckBox ChkRePost;
        private MrMaskedTextBox MskToDate;
        private MrMaskedTextBox MskFrom;
        private MrPanel PanelHeader;
        private MrGroup mrGroup4;
    }
}