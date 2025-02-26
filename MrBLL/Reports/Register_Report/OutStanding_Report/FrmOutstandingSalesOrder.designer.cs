using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.OutStanding_Report
{
    partial class FrmOutstandingSalesOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOutstandingSalesOrder));
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
            this.clsSeparator1 = new ClsSeparator();
            this.ChkSelectAll = new System.Windows.Forms.CheckBox();
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.chk_ViewDxReport = new System.Windows.Forms.CheckBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.mrGroup1 = new MrGroup();
            this.mrPanel1 = new MrPanel();
            this.mrGroup2 = new MrGroup();
            this.mrGroup3 = new MrGroup();
            this.mrGroup1.SuspendLayout();
            this.mrPanel1.SuspendLayout();
            this.mrGroup2.SuspendLayout();
            this.mrGroup3.SuspendLayout();
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
            this.CmbDateType.Location = new System.Drawing.Point(13, 28);
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
            this.MskFrom.Location = new System.Drawing.Point(240, 29);
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
            this.MskToDate.Location = new System.Drawing.Point(375, 29);
            this.MskToDate.Mask = "00/00/0000";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.Size = new System.Drawing.Size(129, 25);
            this.MskToDate.TabIndex = 2;
            this.MskToDate.Validated += new System.EventHandler(this.MskToDate_Validated);
            // 
            // rChkDepartment
            // 
            this.rChkDepartment.AutoSize = true;
            this.rChkDepartment.Location = new System.Drawing.Point(149, 42);
            this.rChkDepartment.Name = "rChkDepartment";
            this.rChkDepartment.Size = new System.Drawing.Size(158, 23);
            this.rChkDepartment.TabIndex = 3;
            this.rChkDepartment.Text = "Department Wise";
            this.rChkDepartment.UseVisualStyleBackColor = true;
            // 
            // rChkArea
            // 
            this.rChkArea.AutoSize = true;
            this.rChkArea.Location = new System.Drawing.Point(318, 17);
            this.rChkArea.Name = "rChkArea";
            this.rChkArea.Size = new System.Drawing.Size(103, 23);
            this.rChkArea.TabIndex = 4;
            this.rChkArea.Text = "Area Wise";
            this.rChkArea.UseVisualStyleBackColor = true;
            // 
            // rChkAgent
            // 
            this.rChkAgent.AutoSize = true;
            this.rChkAgent.Location = new System.Drawing.Point(318, 42);
            this.rChkAgent.Name = "rChkAgent";
            this.rChkAgent.Size = new System.Drawing.Size(111, 23);
            this.rChkAgent.TabIndex = 5;
            this.rChkAgent.Text = "Agent Wise";
            this.rChkAgent.UseVisualStyleBackColor = true;
            // 
            // rChkCustomer
            // 
            this.rChkCustomer.AutoSize = true;
            this.rChkCustomer.Location = new System.Drawing.Point(149, 17);
            this.rChkCustomer.Name = "rChkCustomer";
            this.rChkCustomer.Size = new System.Drawing.Size(141, 23);
            this.rChkCustomer.TabIndex = 2;
            this.rChkCustomer.Text = "Customer Wise";
            this.rChkCustomer.UseVisualStyleBackColor = true;
            // 
            // rChkInvoice
            // 
            this.rChkInvoice.AutoSize = true;
            this.rChkInvoice.Location = new System.Drawing.Point(13, 42);
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
            this.rChkDate.Location = new System.Drawing.Point(13, 17);
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
            this.ChkIncludeNarration.Location = new System.Drawing.Point(13, 119);
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
            this.ChkDocAgent.Location = new System.Drawing.Point(347, 47);
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
            this.ChkSummary.Location = new System.Drawing.Point(13, 23);
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
            this.ChkAdditionalTerm.Location = new System.Drawing.Point(180, 23);
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
            this.ChkFreeQty.Location = new System.Drawing.Point(347, 95);
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
            this.ChkWithAdjustment.Location = new System.Drawing.Point(180, 47);
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
            this.ChkIncludeAltQty.Location = new System.Drawing.Point(347, 71);
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
            this.ChkIncludeOrderNo.Location = new System.Drawing.Point(180, 71);
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
            this.ChkIncludeChallanNo.Location = new System.Drawing.Point(180, 95);
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
            this.ChkIncludeBatch.Location = new System.Drawing.Point(347, 23);
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
            this.ChkIncludeGodown.Location = new System.Drawing.Point(13, 47);
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
            this.ChkDateWise.Location = new System.Drawing.Point(13, 71);
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
            this.ChkIncludeRemarks.Location = new System.Drawing.Point(13, 95);
            this.ChkIncludeRemarks.Name = "ChkIncludeRemarks";
            this.ChkIncludeRemarks.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeRemarks.Size = new System.Drawing.Size(165, 23);
            this.ChkIncludeRemarks.TabIndex = 3;
            this.ChkIncludeRemarks.Text = "Remarks";
            this.ChkIncludeRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeRemarks.UseVisualStyleBackColor = true;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator1.Location = new System.Drawing.Point(8, 146);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(491, 2);
            this.clsSeparator1.TabIndex = 13;
            this.clsSeparator1.TabStop = false;
            // 
            // ChkSelectAll
            // 
            this.ChkSelectAll.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSelectAll.Location = new System.Drawing.Point(347, 119);
            this.ChkSelectAll.Name = "ChkSelectAll";
            this.ChkSelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSelectAll.Size = new System.Drawing.Size(152, 23);
            this.ChkSelectAll.TabIndex = 14;
            this.ChkSelectAll.Text = "Select All";
            this.ChkSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSelectAll.UseVisualStyleBackColor = true;
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.BtnShow.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnShow.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.BtnShow.Location = new System.Drawing.Point(247, 149);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(95, 37);
            this.BtnShow.TabIndex = 15;
            this.BtnShow.Text = "&SHOW";
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // chk_ViewDxReport
            // 
            this.chk_ViewDxReport.AllowDrop = true;
            this.chk_ViewDxReport.Cursor = System.Windows.Forms.Cursors.Default;
            this.chk_ViewDxReport.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_ViewDxReport.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chk_ViewDxReport.Location = new System.Drawing.Point(180, 119);
            this.chk_ViewDxReport.Name = "chk_ViewDxReport";
            this.chk_ViewDxReport.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_ViewDxReport.Size = new System.Drawing.Size(162, 23);
            this.chk_ViewDxReport.TabIndex = 9;
            this.chk_ViewDxReport.Text = "Dynamic Report";
            this.chk_ViewDxReport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_ViewDxReport.UseMnemonic = false;
            this.chk_ViewDxReport.UseVisualStyleBackColor = true;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.BtnCancel.Location = new System.Drawing.Point(344, 149);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(112, 37);
            this.BtnCancel.TabIndex = 16;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.MskFrom);
            this.mrGroup1.Controls.Add(this.CmbDateType);
            this.mrGroup1.Controls.Add(this.MskToDate);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "Date Type";
            this.mrGroup1.Location = new System.Drawing.Point(6, 3);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 5;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(516, 59);
            this.mrGroup1.TabIndex = 0;
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.mrGroup1);
            this.mrPanel1.Controls.Add(this.mrGroup2);
            this.mrPanel1.Controls.Add(this.mrGroup3);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(0, 0);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(526, 308);
            this.mrPanel1.TabIndex = 0;
            // 
            // mrGroup2
            // 
            this.mrGroup2.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup2.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup2.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup2.BorderColor = System.Drawing.Color.White;
            this.mrGroup2.BorderThickness = 1F;
            this.mrGroup2.Controls.Add(this.rChkDepartment);
            this.mrGroup2.Controls.Add(this.rChkDate);
            this.mrGroup2.Controls.Add(this.rChkArea);
            this.mrGroup2.Controls.Add(this.rChkCustomer);
            this.mrGroup2.Controls.Add(this.rChkInvoice);
            this.mrGroup2.Controls.Add(this.rChkAgent);
            this.mrGroup2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup2.GroupImage = null;
            this.mrGroup2.GroupTitle = "";
            this.mrGroup2.Location = new System.Drawing.Point(6, 55);
            this.mrGroup2.Name = "mrGroup2";
            this.mrGroup2.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup2.PaintGroupBox = false;
            this.mrGroup2.RoundCorners = 5;
            this.mrGroup2.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup2.ShadowControl = false;
            this.mrGroup2.ShadowThickness = 3;
            this.mrGroup2.Size = new System.Drawing.Size(516, 65);
            this.mrGroup2.TabIndex = 1;
            // 
            // mrGroup3
            // 
            this.mrGroup3.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup3.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup3.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup3.BorderColor = System.Drawing.Color.White;
            this.mrGroup3.BorderThickness = 1F;
            this.mrGroup3.Controls.Add(this.clsSeparator1);
            this.mrGroup3.Controls.Add(this.ChkSummary);
            this.mrGroup3.Controls.Add(this.ChkIncludeNarration);
            this.mrGroup3.Controls.Add(this.ChkIncludeBatch);
            this.mrGroup3.Controls.Add(this.ChkSelectAll);
            this.mrGroup3.Controls.Add(this.ChkIncludeChallanNo);
            this.mrGroup3.Controls.Add(this.ChkDocAgent);
            this.mrGroup3.Controls.Add(this.ChkIncludeGodown);
            this.mrGroup3.Controls.Add(this.BtnShow);
            this.mrGroup3.Controls.Add(this.ChkIncludeOrderNo);
            this.mrGroup3.Controls.Add(this.ChkDateWise);
            this.mrGroup3.Controls.Add(this.ChkAdditionalTerm);
            this.mrGroup3.Controls.Add(this.ChkIncludeAltQty);
            this.mrGroup3.Controls.Add(this.chk_ViewDxReport);
            this.mrGroup3.Controls.Add(this.ChkIncludeRemarks);
            this.mrGroup3.Controls.Add(this.ChkFreeQty);
            this.mrGroup3.Controls.Add(this.ChkWithAdjustment);
            this.mrGroup3.Controls.Add(this.BtnCancel);
            this.mrGroup3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup3.GroupImage = null;
            this.mrGroup3.GroupTitle = "";
            this.mrGroup3.Location = new System.Drawing.Point(6, 113);
            this.mrGroup3.Name = "mrGroup3";
            this.mrGroup3.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup3.PaintGroupBox = false;
            this.mrGroup3.RoundCorners = 5;
            this.mrGroup3.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup3.ShadowControl = false;
            this.mrGroup3.ShadowThickness = 3;
            this.mrGroup3.Size = new System.Drawing.Size(516, 191);
            this.mrGroup3.TabIndex = 2;
            // 
            // FrmOutstandingSalesOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(526, 308);
            this.Controls.Add(this.mrPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmOutstandingSalesOrder";
            this.Text = "OUTSTANDING SALES CHALLAN";
            this.Load += new System.EventHandler(this.OutstandingSalesChallan_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OutstandingSalesChallan_KeyPress);
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.mrPanel1.ResumeLayout(false);
            this.mrGroup2.ResumeLayout(false);
            this.mrGroup2.PerformLayout();
            this.mrGroup3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox CmbDateType;
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
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private System.Windows.Forms.CheckBox chk_ViewDxReport;
        private System.Windows.Forms.CheckBox ChkSelectAll;
        private System.Windows.Forms.RadioButton rChkInvoice;
        private System.Windows.Forms.CheckBox ChkIncludeNarration;
        private ClsSeparator clsSeparator1;
        private MrGroup mrGroup1;
        private MrPanel mrPanel1;
        private MrMaskedTextBox MskFrom;
        private MrMaskedTextBox MskToDate;
        private MrGroup mrGroup2;
        private MrGroup mrGroup3;
    }
}