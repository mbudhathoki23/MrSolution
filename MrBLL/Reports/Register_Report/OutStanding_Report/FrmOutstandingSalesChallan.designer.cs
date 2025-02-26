using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.OutStanding_Report
{
    partial class FrmOutstandingSalesChallan
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
            this.CmbDateType = new System.Windows.Forms.ComboBox();
            this.MskFrom = new MrMaskedTextBox();
            this.MskToDate = new MrMaskedTextBox();
            this.rChkDepartment = new System.Windows.Forms.RadioButton();
            this.rChkArea = new System.Windows.Forms.RadioButton();
            this.rChkAgent = new System.Windows.Forms.RadioButton();
            this.rChkCustomer = new System.Windows.Forms.RadioButton();
            this.rChkInvoice = new System.Windows.Forms.RadioButton();
            this.rChkDate = new System.Windows.Forms.RadioButton();
            this.ChkIncludeNarration = new System.Windows.Forms.CheckBox();
            this.ChkDocAgent = new System.Windows.Forms.CheckBox();
            this.ChkSummary = new System.Windows.Forms.CheckBox();
            this.ChkAdditionalTerm = new System.Windows.Forms.CheckBox();
            this.ChkFreeQty = new System.Windows.Forms.CheckBox();
            this.ChkWithAdjustment = new System.Windows.Forms.CheckBox();
            this.ChkIncludeAltQty = new System.Windows.Forms.CheckBox();
            this.ChkIncludeOrderNo = new System.Windows.Forms.CheckBox();
            this.ChkIncludeChallanNo = new System.Windows.Forms.CheckBox();
            this.ChkIncludeBatch = new System.Windows.Forms.CheckBox();
            this.ChkIncludeGodown = new System.Windows.Forms.CheckBox();
            this.ChkDateWise = new System.Windows.Forms.CheckBox();
            this.ChkIncludeRemarks = new System.Windows.Forms.CheckBox();
            this.roundPanel6 = new RoundPanel();
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.chk_ViewDxReport = new System.Windows.Forms.CheckBox();
            this.ChkSelectAll = new System.Windows.Forms.CheckBox();
            this.panel1 = new MrPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.clsSeparator1 = new ClsSeparator();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.roundPanel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // CmbDateType
            // 
            this.CmbDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDateType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbDateType.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.CmbDateType.Location = new System.Drawing.Point(6, 15);
            this.CmbDateType.Name = "CmbDateType";
            this.CmbDateType.Size = new System.Drawing.Size(221, 27);
            this.CmbDateType.TabIndex = 0;
            this.CmbDateType.SelectedIndexChanged += new System.EventHandler(this.CmbDateType_SelectedIndexChanged);
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskFrom.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskFrom.Location = new System.Drawing.Point(233, 16);
            this.MskFrom.Mask = "00/00/0000";
            this.MskFrom.Name = "MskFrom";
            this.MskFrom.Size = new System.Drawing.Size(129, 25);
            this.MskFrom.TabIndex = 1;
            this.MskFrom.ValidatingType = typeof(System.DateTime);
            this.MskFrom.Validated += new System.EventHandler(this.MskFrom_Validated);
            // 
            // MskToDate
            // 
            this.MskToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskToDate.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskToDate.Location = new System.Drawing.Point(368, 16);
            this.MskToDate.Mask = "00/00/0000";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.Size = new System.Drawing.Size(129, 25);
            this.MskToDate.TabIndex = 2;
            this.MskToDate.Validated += new System.EventHandler(this.MskToDate_Validated);
            // 
            // rChkDepartment
            // 
            this.rChkDepartment.AutoSize = true;
            this.rChkDepartment.Location = new System.Drawing.Point(149, 39);
            this.rChkDepartment.Name = "rChkDepartment";
            this.rChkDepartment.Size = new System.Drawing.Size(158, 23);
            this.rChkDepartment.TabIndex = 3;
            this.rChkDepartment.Text = "Department Wise";
            this.rChkDepartment.UseVisualStyleBackColor = true;
            // 
            // rChkArea
            // 
            this.rChkArea.AutoSize = true;
            this.rChkArea.Location = new System.Drawing.Point(318, 14);
            this.rChkArea.Name = "rChkArea";
            this.rChkArea.Size = new System.Drawing.Size(103, 23);
            this.rChkArea.TabIndex = 4;
            this.rChkArea.Text = "Area Wise";
            this.rChkArea.UseVisualStyleBackColor = true;
            // 
            // rChkAgent
            // 
            this.rChkAgent.AutoSize = true;
            this.rChkAgent.Location = new System.Drawing.Point(318, 39);
            this.rChkAgent.Name = "rChkAgent";
            this.rChkAgent.Size = new System.Drawing.Size(111, 23);
            this.rChkAgent.TabIndex = 5;
            this.rChkAgent.Text = "Agent Wise";
            this.rChkAgent.UseVisualStyleBackColor = true;
            // 
            // rChkCustomer
            // 
            this.rChkCustomer.AutoSize = true;
            this.rChkCustomer.Location = new System.Drawing.Point(149, 14);
            this.rChkCustomer.Name = "rChkCustomer";
            this.rChkCustomer.Size = new System.Drawing.Size(141, 23);
            this.rChkCustomer.TabIndex = 2;
            this.rChkCustomer.Text = "Customer Wise";
            this.rChkCustomer.UseVisualStyleBackColor = true;
            // 
            // rChkInvoice
            // 
            this.rChkInvoice.AutoSize = true;
            this.rChkInvoice.Location = new System.Drawing.Point(13, 39);
            this.rChkInvoice.Name = "rChkInvoice";
            this.rChkInvoice.Size = new System.Drawing.Size(127, 23);
            this.rChkInvoice.TabIndex = 1;
            this.rChkInvoice.Text = "Challan Wise";
            this.rChkInvoice.UseVisualStyleBackColor = true;
            // 
            // rChkDate
            // 
            this.rChkDate.AutoSize = true;
            this.rChkDate.Checked = true;
            this.rChkDate.Location = new System.Drawing.Point(13, 14);
            this.rChkDate.Name = "rChkDate";
            this.rChkDate.Size = new System.Drawing.Size(104, 23);
            this.rChkDate.TabIndex = 0;
            this.rChkDate.TabStop = true;
            this.rChkDate.Text = "Date Wise";
            this.rChkDate.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeNarration
            // 
            this.ChkIncludeNarration.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeNarration.Location = new System.Drawing.Point(9, 110);
            this.ChkIncludeNarration.Name = "ChkIncludeNarration";
            this.ChkIncludeNarration.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeNarration.Size = new System.Drawing.Size(165, 23);
            this.ChkIncludeNarration.TabIndex = 4;
            this.ChkIncludeNarration.Text = "Description";
            this.ChkIncludeNarration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeNarration.UseVisualStyleBackColor = true;
            // 
            // ChkDocAgent
            // 
            this.ChkDocAgent.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDocAgent.Location = new System.Drawing.Point(343, 38);
            this.ChkDocAgent.Name = "ChkDocAgent";
            this.ChkDocAgent.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDocAgent.Size = new System.Drawing.Size(152, 23);
            this.ChkDocAgent.TabIndex = 11;
            this.ChkDocAgent.Text = "DocAgent";
            this.ChkDocAgent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDocAgent.UseVisualStyleBackColor = true;
            // 
            // ChkSummary
            // 
            this.ChkSummary.Checked = true;
            this.ChkSummary.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkSummary.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSummary.Location = new System.Drawing.Point(9, 14);
            this.ChkSummary.Name = "ChkSummary";
            this.ChkSummary.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSummary.Size = new System.Drawing.Size(165, 23);
            this.ChkSummary.TabIndex = 0;
            this.ChkSummary.Text = "Summary";
            this.ChkSummary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSummary.UseVisualStyleBackColor = true;
            this.ChkSummary.CheckedChanged += new System.EventHandler(this.ChkSummary_CheckedChanged);
            // 
            // ChkAdditionalTerm
            // 
            this.ChkAdditionalTerm.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAdditionalTerm.Location = new System.Drawing.Point(176, 14);
            this.ChkAdditionalTerm.Name = "ChkAdditionalTerm";
            this.ChkAdditionalTerm.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkAdditionalTerm.Size = new System.Drawing.Size(162, 23);
            this.ChkAdditionalTerm.TabIndex = 5;
            this.ChkAdditionalTerm.Text = "Additional Term";
            this.ChkAdditionalTerm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkAdditionalTerm.UseVisualStyleBackColor = true;
            // 
            // ChkFreeQty
            // 
            this.ChkFreeQty.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkFreeQty.Location = new System.Drawing.Point(343, 86);
            this.ChkFreeQty.Name = "ChkFreeQty";
            this.ChkFreeQty.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkFreeQty.Size = new System.Drawing.Size(152, 23);
            this.ChkFreeQty.TabIndex = 13;
            this.ChkFreeQty.Text = "Free Qty";
            this.ChkFreeQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkFreeQty.UseVisualStyleBackColor = true;
            // 
            // ChkWithAdjustment
            // 
            this.ChkWithAdjustment.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkWithAdjustment.Location = new System.Drawing.Point(176, 38);
            this.ChkWithAdjustment.Name = "ChkWithAdjustment";
            this.ChkWithAdjustment.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkWithAdjustment.Size = new System.Drawing.Size(162, 23);
            this.ChkWithAdjustment.TabIndex = 6;
            this.ChkWithAdjustment.Text = "With Adjustment";
            this.ChkWithAdjustment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkWithAdjustment.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeAltQty
            // 
            this.ChkIncludeAltQty.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeAltQty.Location = new System.Drawing.Point(343, 62);
            this.ChkIncludeAltQty.Name = "ChkIncludeAltQty";
            this.ChkIncludeAltQty.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeAltQty.Size = new System.Drawing.Size(152, 23);
            this.ChkIncludeAltQty.TabIndex = 12;
            this.ChkIncludeAltQty.Text = "Alt Qty";
            this.ChkIncludeAltQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeAltQty.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeOrderNo
            // 
            this.ChkIncludeOrderNo.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeOrderNo.Location = new System.Drawing.Point(176, 62);
            this.ChkIncludeOrderNo.Name = "ChkIncludeOrderNo";
            this.ChkIncludeOrderNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeOrderNo.Size = new System.Drawing.Size(162, 23);
            this.ChkIncludeOrderNo.TabIndex = 7;
            this.ChkIncludeOrderNo.Text = "Order No";
            this.ChkIncludeOrderNo.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.ChkIncludeOrderNo.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeChallanNo
            // 
            this.ChkIncludeChallanNo.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeChallanNo.Location = new System.Drawing.Point(176, 86);
            this.ChkIncludeChallanNo.Name = "ChkIncludeChallanNo";
            this.ChkIncludeChallanNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeChallanNo.Size = new System.Drawing.Size(162, 23);
            this.ChkIncludeChallanNo.TabIndex = 8;
            this.ChkIncludeChallanNo.Text = "Quot. No";
            this.ChkIncludeChallanNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeChallanNo.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeBatch
            // 
            this.ChkIncludeBatch.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeBatch.Location = new System.Drawing.Point(343, 14);
            this.ChkIncludeBatch.Name = "ChkIncludeBatch";
            this.ChkIncludeBatch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeBatch.Size = new System.Drawing.Size(152, 23);
            this.ChkIncludeBatch.TabIndex = 10;
            this.ChkIncludeBatch.Text = "Batch";
            this.ChkIncludeBatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeBatch.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeGodown
            // 
            this.ChkIncludeGodown.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeGodown.Location = new System.Drawing.Point(9, 38);
            this.ChkIncludeGodown.Name = "ChkIncludeGodown";
            this.ChkIncludeGodown.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeGodown.Size = new System.Drawing.Size(165, 23);
            this.ChkIncludeGodown.TabIndex = 1;
            this.ChkIncludeGodown.Text = "Godown";
            this.ChkIncludeGodown.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeGodown.UseVisualStyleBackColor = true;
            // 
            // ChkDateWise
            // 
            this.ChkDateWise.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDateWise.Location = new System.Drawing.Point(9, 62);
            this.ChkDateWise.Name = "ChkDateWise";
            this.ChkDateWise.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDateWise.Size = new System.Drawing.Size(165, 23);
            this.ChkDateWise.TabIndex = 2;
            this.ChkDateWise.Text = "Date";
            this.ChkDateWise.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDateWise.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeRemarks
            // 
            this.ChkIncludeRemarks.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeRemarks.Location = new System.Drawing.Point(9, 86);
            this.ChkIncludeRemarks.Name = "ChkIncludeRemarks";
            this.ChkIncludeRemarks.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeRemarks.Size = new System.Drawing.Size(165, 23);
            this.ChkIncludeRemarks.TabIndex = 3;
            this.ChkIncludeRemarks.Text = "Remarks";
            this.ChkIncludeRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeRemarks.UseVisualStyleBackColor = true;
            // 
            // roundPanel6
            // 
            this.roundPanel6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.roundPanel6.Controls.Add(this.panel1);
            this.roundPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roundPanel6.Location = new System.Drawing.Point(0, 0);
            this.roundPanel6.Name = "roundPanel6";
            this.roundPanel6.Radious = 25;
            this.roundPanel6.Size = new System.Drawing.Size(515, 288);
            this.roundPanel6.TabIndex = 0;
            this.roundPanel6.TabStop = false;
            this.roundPanel6.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel6.TitleFont = new System.Drawing.Font("Bookman Old Style", 1F);
            this.roundPanel6.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel6.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.BtnShow.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnShow.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.BtnShow.Location = new System.Drawing.Point(243, 140);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(95, 37);
            this.BtnShow.TabIndex = 15;
            this.BtnShow.Text = "&SHOW";
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.BtnCancel.Location = new System.Drawing.Point(340, 140);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(112, 37);
            this.BtnCancel.TabIndex = 16;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // chk_ViewDxReport
            // 
            this.chk_ViewDxReport.AllowDrop = true;
            this.chk_ViewDxReport.Cursor = System.Windows.Forms.Cursors.Default;
            this.chk_ViewDxReport.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_ViewDxReport.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chk_ViewDxReport.Location = new System.Drawing.Point(176, 110);
            this.chk_ViewDxReport.Name = "chk_ViewDxReport";
            this.chk_ViewDxReport.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_ViewDxReport.Size = new System.Drawing.Size(162, 23);
            this.chk_ViewDxReport.TabIndex = 9;
            this.chk_ViewDxReport.Text = "Dynamic Report";
            this.chk_ViewDxReport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_ViewDxReport.UseMnemonic = false;
            this.chk_ViewDxReport.UseVisualStyleBackColor = true;
            // 
            // ChkSelectAll
            // 
            this.ChkSelectAll.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSelectAll.Location = new System.Drawing.Point(343, 110);
            this.ChkSelectAll.Name = "ChkSelectAll";
            this.ChkSelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSelectAll.Size = new System.Drawing.Size(152, 23);
            this.ChkSelectAll.TabIndex = 14;
            this.ChkSelectAll.Text = "Select All";
            this.ChkSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSelectAll.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(3, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(509, 276);
            this.panel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.clsSeparator1);
            this.groupBox1.Controls.Add(this.ChkIncludeNarration);
            this.groupBox1.Controls.Add(this.ChkSelectAll);
            this.groupBox1.Controls.Add(this.ChkDocAgent);
            this.groupBox1.Controls.Add(this.BtnShow);
            this.groupBox1.Controls.Add(this.ChkSummary);
            this.groupBox1.Controls.Add(this.ChkAdditionalTerm);
            this.groupBox1.Controls.Add(this.chk_ViewDxReport);
            this.groupBox1.Controls.Add(this.ChkFreeQty);
            this.groupBox1.Controls.Add(this.BtnCancel);
            this.groupBox1.Controls.Add(this.ChkWithAdjustment);
            this.groupBox1.Controls.Add(this.ChkIncludeRemarks);
            this.groupBox1.Controls.Add(this.ChkIncludeAltQty);
            this.groupBox1.Controls.Add(this.ChkDateWise);
            this.groupBox1.Controls.Add(this.ChkIncludeOrderNo);
            this.groupBox1.Controls.Add(this.ChkIncludeGodown);
            this.groupBox1.Controls.Add(this.ChkIncludeChallanNo);
            this.groupBox1.Controls.Add(this.ChkIncludeBatch);
            this.groupBox1.Location = new System.Drawing.Point(1, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(503, 181);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(4, 137);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(491, 2);
            this.clsSeparator1.TabIndex = 13;
            this.clsSeparator1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rChkDepartment);
            this.groupBox2.Controls.Add(this.rChkArea);
            this.groupBox2.Controls.Add(this.rChkDate);
            this.groupBox2.Controls.Add(this.rChkAgent);
            this.groupBox2.Controls.Add(this.rChkInvoice);
            this.groupBox2.Controls.Add(this.rChkCustomer);
            this.groupBox2.Location = new System.Drawing.Point(2, 33);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(502, 67);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.MskFrom);
            this.groupBox3.Controls.Add(this.MskToDate);
            this.groupBox3.Controls.Add(this.CmbDateType);
            this.groupBox3.Location = new System.Drawing.Point(3, -7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(502, 47);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // FrmOutstandingSalesChallan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(515, 288);
            this.Controls.Add(this.roundPanel6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmOutstandingSalesChallan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OUTSTANDING SALES CHALLAN";
            this.Load += new System.EventHandler(this.OutstandingSalesChallan_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OutstandingSalesChallan_KeyPress);
            this.roundPanel6.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox CmbDateType;
        private System.Windows.Forms.MaskedTextBox MskFrom;
        private System.Windows.Forms.MaskedTextBox MskToDate;
        private System.Windows.Forms.RadioButton rChkDepartment;
        private System.Windows.Forms.RadioButton rChkArea;
        private System.Windows.Forms.RadioButton rChkAgent;
        private System.Windows.Forms.RadioButton rChkCustomer;
        private System.Windows.Forms.RadioButton rChkDate;
        private System.Windows.Forms.CheckBox ChkDocAgent;
        private System.Windows.Forms.CheckBox ChkSummary;
        private System.Windows.Forms.CheckBox ChkAdditionalTerm;
        private System.Windows.Forms.CheckBox ChkFreeQty;
        private System.Windows.Forms.CheckBox ChkWithAdjustment;
        private System.Windows.Forms.CheckBox ChkIncludeAltQty;
        private System.Windows.Forms.CheckBox ChkIncludeOrderNo;
        private System.Windows.Forms.CheckBox ChkIncludeChallanNo;
        private System.Windows.Forms.CheckBox ChkIncludeBatch;
        private System.Windows.Forms.CheckBox ChkIncludeGodown;
        private System.Windows.Forms.CheckBox ChkDateWise;
        private System.Windows.Forms.CheckBox ChkIncludeRemarks;
        private RoundPanel roundPanel6;
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private System.Windows.Forms.CheckBox chk_ViewDxReport;
        private System.Windows.Forms.CheckBox ChkSelectAll;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rChkInvoice;
        private System.Windows.Forms.CheckBox ChkIncludeNarration;
        private System.Windows.Forms.GroupBox groupBox1;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}