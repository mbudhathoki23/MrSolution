using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.Analysis_Report.Sales
{
    partial class FrmSalesAnalysis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSalesAnalysis));
            this.ChkDocumentAgent = new System.Windows.Forms.CheckBox();
            this.rChkAgent = new System.Windows.Forms.RadioButton();
            this.rChkCustomer = new System.Windows.Forms.RadioButton();
            this.rChkProduct = new System.Windows.Forms.RadioButton();
            this.ChkDate = new System.Windows.Forms.CheckBox();
            this.ChkAdditionalTerm = new System.Windows.Forms.CheckBox();
            this.PanelHeader = new MrPanel();
            this.mrGroup3 = new MrGroup();
            this.chk_SelectAll = new System.Windows.Forms.CheckBox();
            this.chk_ViewDxReport = new System.Windows.Forms.CheckBox();
            this.ChkMainArea = new System.Windows.Forms.CheckBox();
            this.ChkMainAgent = new System.Windows.Forms.CheckBox();
            this.ChkIncludeVatAmount = new System.Windows.Forms.CheckBox();
            this.ChkIncudeProduct = new System.Windows.Forms.CheckBox();
            this.ChkIncludeCustomer = new System.Windows.Forms.CheckBox();
            this.mrGroup2 = new MrGroup();
            this.rChkProductSubGroup = new System.Windows.Forms.RadioButton();
            this.rChkProductGroup = new System.Windows.Forms.RadioButton();
            this.rChkArea = new System.Windows.Forms.RadioButton();
            this.mrGroup1 = new MrGroup();
            this.CmbDateType = new System.Windows.Forms.ComboBox();
            this.MskFrom = new MrMaskedTextBox();
            this.MskToDate = new MrMaskedTextBox();
            this.mrGroup4 = new MrGroup();
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.PanelHeader.SuspendLayout();
            this.mrGroup3.SuspendLayout();
            this.mrGroup2.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            this.mrGroup4.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChkDocumentAgent
            // 
            this.ChkDocumentAgent.Enabled = false;
            this.ChkDocumentAgent.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDocumentAgent.Location = new System.Drawing.Point(7, 79);
            this.ChkDocumentAgent.Name = "ChkDocumentAgent";
            this.ChkDocumentAgent.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDocumentAgent.Size = new System.Drawing.Size(169, 23);
            this.ChkDocumentAgent.TabIndex = 4;
            this.ChkDocumentAgent.Text = "Bill Agent";
            this.ChkDocumentAgent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDocumentAgent.UseVisualStyleBackColor = true;
            // 
            // rChkAgent
            // 
            this.rChkAgent.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChkAgent.Location = new System.Drawing.Point(9, 150);
            this.rChkAgent.Name = "rChkAgent";
            this.rChkAgent.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rChkAgent.Size = new System.Drawing.Size(166, 24);
            this.rChkAgent.TabIndex = 6;
            this.rChkAgent.Text = "Agent Wise";
            this.rChkAgent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rChkAgent.UseVisualStyleBackColor = true;
            this.rChkAgent.CheckedChanged += new System.EventHandler(this.rChkAgent_CheckedChanged);
            // 
            // rChkCustomer
            // 
            this.rChkCustomer.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChkCustomer.Location = new System.Drawing.Point(9, 101);
            this.rChkCustomer.Name = "rChkCustomer";
            this.rChkCustomer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rChkCustomer.Size = new System.Drawing.Size(166, 24);
            this.rChkCustomer.TabIndex = 3;
            this.rChkCustomer.Text = "Customer Wise";
            this.rChkCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rChkCustomer.UseVisualStyleBackColor = true;
            this.rChkCustomer.CheckedChanged += new System.EventHandler(this.RChkCustomer_CheckedChanged);
            // 
            // rChkProduct
            // 
            this.rChkProduct.Checked = true;
            this.rChkProduct.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChkProduct.Location = new System.Drawing.Point(9, 29);
            this.rChkProduct.Name = "rChkProduct";
            this.rChkProduct.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rChkProduct.Size = new System.Drawing.Size(166, 24);
            this.rChkProduct.TabIndex = 0;
            this.rChkProduct.TabStop = true;
            this.rChkProduct.Text = "Product Wise";
            this.rChkProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rChkProduct.UseVisualStyleBackColor = true;
            this.rChkProduct.CheckedChanged += new System.EventHandler(this.RChkProduct_CheckedChanged);
            // 
            // ChkDate
            // 
            this.ChkDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDate.Location = new System.Drawing.Point(7, 103);
            this.ChkDate.Name = "ChkDate";
            this.ChkDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDate.Size = new System.Drawing.Size(169, 23);
            this.ChkDate.TabIndex = 5;
            this.ChkDate.Text = "Date";
            this.ChkDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDate.UseVisualStyleBackColor = true;
            // 
            // ChkAdditionalTerm
            // 
            this.ChkAdditionalTerm.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAdditionalTerm.Location = new System.Drawing.Point(181, 55);
            this.ChkAdditionalTerm.Name = "ChkAdditionalTerm";
            this.ChkAdditionalTerm.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkAdditionalTerm.Size = new System.Drawing.Size(169, 23);
            this.ChkAdditionalTerm.TabIndex = 3;
            this.ChkAdditionalTerm.Text = "Include Add Term";
            this.ChkAdditionalTerm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkAdditionalTerm.UseVisualStyleBackColor = true;
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.mrGroup3);
            this.PanelHeader.Controls.Add(this.mrGroup2);
            this.PanelHeader.Controls.Add(this.mrGroup1);
            this.PanelHeader.Controls.Add(this.mrGroup4);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(578, 276);
            this.PanelHeader.TabIndex = 0;
            // 
            // mrGroup3
            // 
            this.mrGroup3.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup3.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup3.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup3.BorderColor = System.Drawing.Color.White;
            this.mrGroup3.BorderThickness = 1F;
            this.mrGroup3.Controls.Add(this.chk_SelectAll);
            this.mrGroup3.Controls.Add(this.chk_ViewDxReport);
            this.mrGroup3.Controls.Add(this.ChkMainArea);
            this.mrGroup3.Controls.Add(this.ChkMainAgent);
            this.mrGroup3.Controls.Add(this.ChkDate);
            this.mrGroup3.Controls.Add(this.ChkIncludeVatAmount);
            this.mrGroup3.Controls.Add(this.ChkAdditionalTerm);
            this.mrGroup3.Controls.Add(this.ChkDocumentAgent);
            this.mrGroup3.Controls.Add(this.ChkIncudeProduct);
            this.mrGroup3.Controls.Add(this.ChkIncludeCustomer);
            this.mrGroup3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup3.GroupImage = null;
            this.mrGroup3.GroupTitle = "Filter Value";
            this.mrGroup3.Location = new System.Drawing.Point(217, 39);
            this.mrGroup3.Name = "mrGroup3";
            this.mrGroup3.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup3.PaintGroupBox = false;
            this.mrGroup3.RoundCorners = 5;
            this.mrGroup3.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup3.ShadowControl = false;
            this.mrGroup3.ShadowThickness = 3;
            this.mrGroup3.Size = new System.Drawing.Size(354, 187);
            this.mrGroup3.TabIndex = 6;
            // 
            // chk_SelectAll
            // 
            this.chk_SelectAll.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_SelectAll.Location = new System.Drawing.Point(182, 127);
            this.chk_SelectAll.Name = "chk_SelectAll";
            this.chk_SelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_SelectAll.Size = new System.Drawing.Size(169, 23);
            this.chk_SelectAll.TabIndex = 3;
            this.chk_SelectAll.Text = "Select All";
            this.chk_SelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_SelectAll.UseVisualStyleBackColor = true;
            // 
            // chk_ViewDxReport
            // 
            this.chk_ViewDxReport.AllowDrop = true;
            this.chk_ViewDxReport.Cursor = System.Windows.Forms.Cursors.Default;
            this.chk_ViewDxReport.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_ViewDxReport.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chk_ViewDxReport.Location = new System.Drawing.Point(7, 127);
            this.chk_ViewDxReport.Name = "chk_ViewDxReport";
            this.chk_ViewDxReport.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_ViewDxReport.Size = new System.Drawing.Size(169, 23);
            this.chk_ViewDxReport.TabIndex = 2;
            this.chk_ViewDxReport.Text = "Dynamic Report";
            this.chk_ViewDxReport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_ViewDxReport.UseMnemonic = false;
            this.chk_ViewDxReport.UseVisualStyleBackColor = true;
            // 
            // ChkMainArea
            // 
            this.ChkMainArea.Enabled = false;
            this.ChkMainArea.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkMainArea.Location = new System.Drawing.Point(182, 103);
            this.ChkMainArea.Name = "ChkMainArea";
            this.ChkMainArea.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkMainArea.Size = new System.Drawing.Size(169, 23);
            this.ChkMainArea.TabIndex = 9;
            this.ChkMainArea.Text = "Main Area";
            this.ChkMainArea.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkMainArea.UseVisualStyleBackColor = true;
            // 
            // ChkMainAgent
            // 
            this.ChkMainAgent.Enabled = false;
            this.ChkMainAgent.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkMainAgent.Location = new System.Drawing.Point(181, 79);
            this.ChkMainAgent.Name = "ChkMainAgent";
            this.ChkMainAgent.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkMainAgent.Size = new System.Drawing.Size(169, 23);
            this.ChkMainAgent.TabIndex = 8;
            this.ChkMainAgent.Text = "Main Agent";
            this.ChkMainAgent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkMainAgent.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeVatAmount
            // 
            this.ChkIncludeVatAmount.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeVatAmount.Location = new System.Drawing.Point(181, 31);
            this.ChkIncludeVatAmount.Name = "ChkIncludeVatAmount";
            this.ChkIncludeVatAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeVatAmount.Size = new System.Drawing.Size(169, 23);
            this.ChkIncludeVatAmount.TabIndex = 7;
            this.ChkIncludeVatAmount.Text = "Include Tax";
            this.ChkIncludeVatAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeVatAmount.UseVisualStyleBackColor = true;
            // 
            // ChkIncudeProduct
            // 
            this.ChkIncudeProduct.Enabled = false;
            this.ChkIncudeProduct.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncudeProduct.Location = new System.Drawing.Point(7, 31);
            this.ChkIncudeProduct.Name = "ChkIncudeProduct";
            this.ChkIncudeProduct.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncudeProduct.Size = new System.Drawing.Size(169, 23);
            this.ChkIncudeProduct.TabIndex = 0;
            this.ChkIncudeProduct.Text = "Include Product";
            this.ChkIncudeProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncudeProduct.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeCustomer
            // 
            this.ChkIncludeCustomer.Enabled = false;
            this.ChkIncludeCustomer.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeCustomer.Location = new System.Drawing.Point(7, 55);
            this.ChkIncludeCustomer.Name = "ChkIncludeCustomer";
            this.ChkIncludeCustomer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeCustomer.Size = new System.Drawing.Size(169, 23);
            this.ChkIncludeCustomer.TabIndex = 1;
            this.ChkIncludeCustomer.Text = "Include Customer";
            this.ChkIncludeCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeCustomer.UseVisualStyleBackColor = true;
            // 
            // mrGroup2
            // 
            this.mrGroup2.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup2.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup2.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup2.BorderColor = System.Drawing.Color.White;
            this.mrGroup2.BorderThickness = 1F;
            this.mrGroup2.Controls.Add(this.rChkProductSubGroup);
            this.mrGroup2.Controls.Add(this.rChkProduct);
            this.mrGroup2.Controls.Add(this.rChkProductGroup);
            this.mrGroup2.Controls.Add(this.rChkArea);
            this.mrGroup2.Controls.Add(this.rChkAgent);
            this.mrGroup2.Controls.Add(this.rChkCustomer);
            this.mrGroup2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup2.GroupImage = null;
            this.mrGroup2.GroupTitle = "Report Type";
            this.mrGroup2.Location = new System.Drawing.Point(3, 39);
            this.mrGroup2.Name = "mrGroup2";
            this.mrGroup2.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup2.PaintGroupBox = false;
            this.mrGroup2.RoundCorners = 10;
            this.mrGroup2.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup2.ShadowControl = false;
            this.mrGroup2.ShadowThickness = 3;
            this.mrGroup2.Size = new System.Drawing.Size(208, 187);
            this.mrGroup2.TabIndex = 5;
            // 
            // rChkProductSubGroup
            // 
            this.rChkProductSubGroup.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChkProductSubGroup.Location = new System.Drawing.Point(9, 77);
            this.rChkProductSubGroup.Name = "rChkProductSubGroup";
            this.rChkProductSubGroup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rChkProductSubGroup.Size = new System.Drawing.Size(166, 24);
            this.rChkProductSubGroup.TabIndex = 2;
            this.rChkProductSubGroup.Text = "SubGroup Wise";
            this.rChkProductSubGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rChkProductSubGroup.UseVisualStyleBackColor = true;
            this.rChkProductSubGroup.CheckedChanged += new System.EventHandler(this.RChkProductSubGroup_CheckedChanged);
            // 
            // rChkProductGroup
            // 
            this.rChkProductGroup.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChkProductGroup.Location = new System.Drawing.Point(9, 53);
            this.rChkProductGroup.Name = "rChkProductGroup";
            this.rChkProductGroup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rChkProductGroup.Size = new System.Drawing.Size(166, 24);
            this.rChkProductGroup.TabIndex = 1;
            this.rChkProductGroup.Text = "Group Wise";
            this.rChkProductGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rChkProductGroup.UseVisualStyleBackColor = true;
            this.rChkProductGroup.CheckedChanged += new System.EventHandler(this.RChkProductGroup_CheckedChanged);
            // 
            // rChkArea
            // 
            this.rChkArea.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChkArea.Location = new System.Drawing.Point(9, 125);
            this.rChkArea.Name = "rChkArea";
            this.rChkArea.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rChkArea.Size = new System.Drawing.Size(166, 24);
            this.rChkArea.TabIndex = 4;
            this.rChkArea.Text = "Area Wise";
            this.rChkArea.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rChkArea.UseVisualStyleBackColor = true;
            this.rChkArea.CheckedChanged += new System.EventHandler(this.rChkArea_CheckedChanged);
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
            this.mrGroup1.Controls.Add(this.MskToDate);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "";
            this.mrGroup1.Location = new System.Drawing.Point(3, -10);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 2;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(568, 45);
            this.mrGroup1.TabIndex = 4;
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
            this.CmbDateType.Location = new System.Drawing.Point(9, 14);
            this.CmbDateType.Name = "CmbDateType";
            this.CmbDateType.Size = new System.Drawing.Size(264, 27);
            this.CmbDateType.TabIndex = 0;
            this.CmbDateType.SelectedIndexChanged += new System.EventHandler(this.CmbDateType_SelectedIndexChanged);
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskFrom.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskFrom.Location = new System.Drawing.Point(279, 14);
            this.MskFrom.Mask = "00/00/0000";
            this.MskFrom.Name = "MskFrom";
            this.MskFrom.Size = new System.Drawing.Size(129, 25);
            this.MskFrom.TabIndex = 1;
            this.MskFrom.ValidatingType = typeof(System.DateTime);
            this.MskFrom.Validating += new System.ComponentModel.CancelEventHandler(this.MskFrom_Validating);
            // 
            // MskToDate
            // 
            this.MskToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskToDate.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskToDate.Location = new System.Drawing.Point(409, 14);
            this.MskToDate.Mask = "00/00/0000";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.Size = new System.Drawing.Size(129, 25);
            this.MskToDate.TabIndex = 2;
            this.MskToDate.Validating += new System.ComponentModel.CancelEventHandler(this.MskToDate_Validating);
            // 
            // mrGroup4
            // 
            this.mrGroup4.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup4.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup4.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup4.BorderColor = System.Drawing.Color.White;
            this.mrGroup4.BorderThickness = 1F;
            this.mrGroup4.Controls.Add(this.BtnShow);
            this.mrGroup4.Controls.Add(this.BtnCancel);
            this.mrGroup4.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup4.GroupImage = null;
            this.mrGroup4.GroupTitle = "";
            this.mrGroup4.Location = new System.Drawing.Point(6, 218);
            this.mrGroup4.Name = "mrGroup4";
            this.mrGroup4.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup4.PaintGroupBox = false;
            this.mrGroup4.RoundCorners = 2;
            this.mrGroup4.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup4.ShadowControl = false;
            this.mrGroup4.ShadowThickness = 3;
            this.mrGroup4.Size = new System.Drawing.Size(565, 54);
            this.mrGroup4.TabIndex = 7;
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnShow.Location = new System.Drawing.Point(276, 15);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(102, 36);
            this.BtnShow.TabIndex = 0;
            this.BtnShow.Text = "&SHOW";
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(378, 15);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(102, 36);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // FrmSalesAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 276);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmSalesAnalysis";
            this.Text = "SALES ANALYSIS REPORT";
            this.Load += new System.EventHandler(this.FrmSalesAnalysis_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmSalesAnalysis_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            this.mrGroup3.ResumeLayout(false);
            this.mrGroup2.ResumeLayout(false);
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.mrGroup4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox ChkDocumentAgent;
        private System.Windows.Forms.RadioButton rChkAgent;
        private System.Windows.Forms.RadioButton rChkCustomer;
        private System.Windows.Forms.RadioButton rChkProduct;
        private System.Windows.Forms.CheckBox ChkDate;
        private System.Windows.Forms.CheckBox ChkAdditionalTerm;
		private DevExpress.XtraEditors.SimpleButton BtnCancel;
		private DevExpress.XtraEditors.SimpleButton BtnShow;
        private System.Windows.Forms.ComboBox CmbDateType;
        private System.Windows.Forms.RadioButton rChkArea;
        private System.Windows.Forms.RadioButton rChkProductSubGroup;
        private System.Windows.Forms.RadioButton rChkProductGroup;
        private System.Windows.Forms.CheckBox ChkIncudeProduct;
        private System.Windows.Forms.CheckBox ChkIncludeCustomer;
        private System.Windows.Forms.CheckBox chk_ViewDxReport;
        private System.Windows.Forms.CheckBox chk_SelectAll;
        private System.Windows.Forms.CheckBox ChkIncludeVatAmount;
        private MrGroup mrGroup1;
        private MrPanel PanelHeader;
        private MrMaskedTextBox MskFrom;
        private MrMaskedTextBox MskToDate;
        private MrGroup mrGroup2;
        private MrGroup mrGroup3;
        private System.Windows.Forms.CheckBox ChkMainAgent;
        private System.Windows.Forms.CheckBox ChkMainArea;
        private MrGroup mrGroup4;
    }
}