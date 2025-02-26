using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.Aging_Report
{
    partial class FrmAgingReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAgingReport));
            this.lbl_Slab = new System.Windows.Forms.Label();
            this.TxtAgeingSlabDays = new MrTextBox();
            this.ChkDocumentAgent = new System.Windows.Forms.CheckBox();
            this.ChkDate = new System.Windows.Forms.CheckBox();
            this.ChkSelectAll = new System.Windows.Forms.CheckBox();
            this.ChkAgentOnly = new System.Windows.Forms.CheckBox();
            this.lbl_AsOnDate = new System.Windows.Forms.Label();
            this.MskAsOnDate = new MrMaskedTextBox();
            this.lbl_Opening = new System.Windows.Forms.Label();
            this.CmbReportMode = new System.Windows.Forms.ComboBox();
            this.Lbl_On = new System.Windows.Forms.Label();
            this.CmbAgingOn = new System.Windows.Forms.ComboBox();
            this.lbl_Columns = new System.Windows.Forms.Label();
            this.CmbColumnNumber = new System.Windows.Forms.ComboBox();
            this.CmbAgingSlab = new System.Windows.Forms.ComboBox();
            this.lbl_SlabType = new System.Windows.Forms.Label();
            this.ChkIncludeCreditLimit = new System.Windows.Forms.CheckBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.ChkIncludeSales = new System.Windows.Forms.CheckBox();
            this.clsSeparator3 = new ClsSeparator();
            this.PanelHeader = new MrPanel();
            this.mrGroup4 = new MrGroup();
            this.mrGroup2 = new MrGroup();
            this.RChkArea = new System.Windows.Forms.RadioButton();
            this.RChkAgent = new System.Windows.Forms.RadioButton();
            this.RChkAccountSubGroup = new System.Windows.Forms.RadioButton();
            this.RChkAccountGroup = new System.Windows.Forms.RadioButton();
            this.RChkNormal = new System.Windows.Forms.RadioButton();
            this.mrGroup1 = new MrGroup();
            this.mrGroup3 = new MrGroup();
            this.PanelHeader.SuspendLayout();
            this.mrGroup4.SuspendLayout();
            this.mrGroup2.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            this.mrGroup3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Slab
            // 
            this.lbl_Slab.AutoSize = true;
            this.lbl_Slab.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_Slab.Location = new System.Drawing.Point(8, 94);
            this.lbl_Slab.Name = "lbl_Slab";
            this.lbl_Slab.Size = new System.Drawing.Size(41, 19);
            this.lbl_Slab.TabIndex = 80;
            this.lbl_Slab.Text = "Slab";
            // 
            // TxtAgeingSlabDays
            // 
            this.TxtAgeingSlabDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAgeingSlabDays.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtAgeingSlabDays.Location = new System.Drawing.Point(93, 91);
            this.TxtAgeingSlabDays.Name = "TxtAgeingSlabDays";
            this.TxtAgeingSlabDays.Size = new System.Drawing.Size(152, 25);
            this.TxtAgeingSlabDays.TabIndex = 2;
            this.TxtAgeingSlabDays.Text = "30";
            this.TxtAgeingSlabDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtAgeingSlabDays.TextBoxType = TextBoxType.Numeric;
            // 
            // ChkDocumentAgent
            // 
            this.ChkDocumentAgent.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkDocumentAgent.Location = new System.Drawing.Point(178, 17);
            this.ChkDocumentAgent.Name = "ChkDocumentAgent";
            this.ChkDocumentAgent.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDocumentAgent.Size = new System.Drawing.Size(157, 23);
            this.ChkDocumentAgent.TabIndex = 1;
            this.ChkDocumentAgent.Text = "Bill Agent";
            this.ChkDocumentAgent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDocumentAgent.UseVisualStyleBackColor = true;
            // 
            // ChkDate
            // 
            this.ChkDate.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkDate.Location = new System.Drawing.Point(178, 46);
            this.ChkDate.Name = "ChkDate";
            this.ChkDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDate.Size = new System.Drawing.Size(157, 23);
            this.ChkDate.TabIndex = 4;
            this.ChkDate.Text = "Date";
            this.ChkDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDate.UseVisualStyleBackColor = true;
            // 
            // ChkSelectAll
            // 
            this.ChkSelectAll.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkSelectAll.Location = new System.Drawing.Point(353, 46);
            this.ChkSelectAll.Name = "ChkSelectAll";
            this.ChkSelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSelectAll.Size = new System.Drawing.Size(157, 23);
            this.ChkSelectAll.TabIndex = 5;
            this.ChkSelectAll.Text = "Select All";
            this.ChkSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSelectAll.UseVisualStyleBackColor = true;
            // 
            // ChkAgentOnly
            // 
            this.ChkAgentOnly.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkAgentOnly.Location = new System.Drawing.Point(10, 17);
            this.ChkAgentOnly.Name = "ChkAgentOnly";
            this.ChkAgentOnly.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkAgentOnly.Size = new System.Drawing.Size(157, 23);
            this.ChkAgentOnly.TabIndex = 0;
            this.ChkAgentOnly.Text = "Agent Only";
            this.ChkAgentOnly.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkAgentOnly.UseVisualStyleBackColor = true;
            // 
            // lbl_AsOnDate
            // 
            this.lbl_AsOnDate.AutoSize = true;
            this.lbl_AsOnDate.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_AsOnDate.Location = new System.Drawing.Point(299, 34);
            this.lbl_AsOnDate.Name = "lbl_AsOnDate";
            this.lbl_AsOnDate.Size = new System.Drawing.Size(95, 19);
            this.lbl_AsOnDate.TabIndex = 86;
            this.lbl_AsOnDate.Text = "As On Date";
            // 
            // MskAsOnDate
            // 
            this.MskAsOnDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskAsOnDate.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MskAsOnDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskAsOnDate.Location = new System.Drawing.Point(399, 31);
            this.MskAsOnDate.Mask = "00/00/0000";
            this.MskAsOnDate.Name = "MskAsOnDate";
            this.MskAsOnDate.Size = new System.Drawing.Size(124, 25);
            this.MskAsOnDate.TabIndex = 1;
            // 
            // lbl_Opening
            // 
            this.lbl_Opening.AutoSize = true;
            this.lbl_Opening.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_Opening.Location = new System.Drawing.Point(6, 34);
            this.lbl_Opening.Name = "lbl_Opening";
            this.lbl_Opening.Size = new System.Drawing.Size(71, 19);
            this.lbl_Opening.TabIndex = 88;
            this.lbl_Opening.Text = "Opening";
            // 
            // CmbReportMode
            // 
            this.CmbReportMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbReportMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbReportMode.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.CmbReportMode.FormattingEnabled = true;
            this.CmbReportMode.Items.AddRange(new object[] {
            "Include",
            "Exclude",
            "Only"});
            this.CmbReportMode.Location = new System.Drawing.Point(82, 30);
            this.CmbReportMode.Name = "CmbReportMode";
            this.CmbReportMode.Size = new System.Drawing.Size(197, 27);
            this.CmbReportMode.TabIndex = 0;
            this.CmbReportMode.SelectedIndexChanged += new System.EventHandler(this.CmbReportMode_SelectedIndexChanged);
            // 
            // Lbl_On
            // 
            this.Lbl_On.AutoSize = true;
            this.Lbl_On.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.Lbl_On.Location = new System.Drawing.Point(8, 34);
            this.Lbl_On.Name = "Lbl_On";
            this.Lbl_On.Size = new System.Drawing.Size(31, 19);
            this.Lbl_On.TabIndex = 90;
            this.Lbl_On.Text = "On";
            // 
            // CmbAgingOn
            // 
            this.CmbAgingOn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbAgingOn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbAgingOn.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.CmbAgingOn.FormattingEnabled = true;
            this.CmbAgingOn.Items.AddRange(new object[] {
            "Due Date",
            "Bill Date"});
            this.CmbAgingOn.Location = new System.Drawing.Point(93, 30);
            this.CmbAgingOn.Name = "CmbAgingOn";
            this.CmbAgingOn.Size = new System.Drawing.Size(198, 27);
            this.CmbAgingOn.TabIndex = 0;
            // 
            // lbl_Columns
            // 
            this.lbl_Columns.AutoSize = true;
            this.lbl_Columns.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_Columns.Location = new System.Drawing.Point(8, 122);
            this.lbl_Columns.Name = "lbl_Columns";
            this.lbl_Columns.Size = new System.Drawing.Size(75, 19);
            this.lbl_Columns.TabIndex = 92;
            this.lbl_Columns.Text = "Columns";
            // 
            // CmbColumnNumber
            // 
            this.CmbColumnNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbColumnNumber.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbColumnNumber.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.CmbColumnNumber.FormattingEnabled = true;
            this.CmbColumnNumber.Items.AddRange(new object[] {
            "4",
            "7"});
            this.CmbColumnNumber.Location = new System.Drawing.Point(93, 119);
            this.CmbColumnNumber.Name = "CmbColumnNumber";
            this.CmbColumnNumber.Size = new System.Drawing.Size(198, 27);
            this.CmbColumnNumber.TabIndex = 3;
            // 
            // CmbAgingSlab
            // 
            this.CmbAgingSlab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbAgingSlab.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbAgingSlab.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.CmbAgingSlab.FormattingEnabled = true;
            this.CmbAgingSlab.Items.AddRange(new object[] {
            "Fixed",
            "Varient"});
            this.CmbAgingSlab.Location = new System.Drawing.Point(93, 61);
            this.CmbAgingSlab.Name = "CmbAgingSlab";
            this.CmbAgingSlab.Size = new System.Drawing.Size(198, 27);
            this.CmbAgingSlab.TabIndex = 1;
            this.CmbAgingSlab.SelectedIndexChanged += new System.EventHandler(this.CmbAgingSlab_SelectedIndexChanged);
            // 
            // lbl_SlabType
            // 
            this.lbl_SlabType.AutoSize = true;
            this.lbl_SlabType.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_SlabType.Location = new System.Drawing.Point(8, 65);
            this.lbl_SlabType.Name = "lbl_SlabType";
            this.lbl_SlabType.Size = new System.Drawing.Size(80, 19);
            this.lbl_SlabType.TabIndex = 90;
            this.lbl_SlabType.Text = "Slab Type";
            // 
            // ChkIncludeCreditLimit
            // 
            this.ChkIncludeCreditLimit.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkIncludeCreditLimit.Location = new System.Drawing.Point(10, 46);
            this.ChkIncludeCreditLimit.Name = "ChkIncludeCreditLimit";
            this.ChkIncludeCreditLimit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeCreditLimit.Size = new System.Drawing.Size(157, 23);
            this.ChkIncludeCreditLimit.TabIndex = 3;
            this.ChkIncludeCreditLimit.Text = "Credit Limit";
            this.ChkIncludeCreditLimit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeCreditLimit.UseVisualStyleBackColor = true;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(308, 75);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(113, 36);
            this.BtnCancel.TabIndex = 7;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnShow.Location = new System.Drawing.Point(197, 75);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(111, 36);
            this.BtnShow.TabIndex = 6;
            this.BtnShow.Text = "&SHOW";
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // ChkIncludeSales
            // 
            this.ChkIncludeSales.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkIncludeSales.Location = new System.Drawing.Point(353, 17);
            this.ChkIncludeSales.Name = "ChkIncludeSales";
            this.ChkIncludeSales.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeSales.Size = new System.Drawing.Size(157, 23);
            this.ChkIncludeSales.TabIndex = 2;
            this.ChkIncludeSales.Text = "Including Sales";
            this.ChkIncludeSales.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeSales.UseVisualStyleBackColor = true;
            // 
            // clsSeparator3
            // 
            this.clsSeparator3.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator3.Location = new System.Drawing.Point(5, 70);
            this.clsSeparator3.Name = "clsSeparator3";
            this.clsSeparator3.Size = new System.Drawing.Size(533, 2);
            this.clsSeparator3.TabIndex = 67;
            this.clsSeparator3.TabStop = false;
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.mrGroup4);
            this.PanelHeader.Controls.Add(this.mrGroup2);
            this.PanelHeader.Controls.Add(this.mrGroup1);
            this.PanelHeader.Controls.Add(this.mrGroup3);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(554, 345);
            this.PanelHeader.TabIndex = 0;
            // 
            // mrGroup4
            // 
            this.mrGroup4.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup4.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup4.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup4.BorderColor = System.Drawing.Color.White;
            this.mrGroup4.BorderThickness = 1F;
            this.mrGroup4.Controls.Add(this.CmbAgingOn);
            this.mrGroup4.Controls.Add(this.Lbl_On);
            this.mrGroup4.Controls.Add(this.CmbAgingSlab);
            this.mrGroup4.Controls.Add(this.lbl_SlabType);
            this.mrGroup4.Controls.Add(this.TxtAgeingSlabDays);
            this.mrGroup4.Controls.Add(this.lbl_Slab);
            this.mrGroup4.Controls.Add(this.lbl_Columns);
            this.mrGroup4.Controls.Add(this.CmbColumnNumber);
            this.mrGroup4.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup4.GroupImage = null;
            this.mrGroup4.GroupTitle = "Filter";
            this.mrGroup4.Location = new System.Drawing.Point(250, 72);
            this.mrGroup4.Name = "mrGroup4";
            this.mrGroup4.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup4.PaintGroupBox = false;
            this.mrGroup4.RoundCorners = 10;
            this.mrGroup4.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup4.ShadowControl = false;
            this.mrGroup4.ShadowThickness = 3;
            this.mrGroup4.Size = new System.Drawing.Size(299, 164);
            this.mrGroup4.TabIndex = 2;
            // 
            // mrGroup2
            // 
            this.mrGroup2.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup2.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup2.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup2.BorderColor = System.Drawing.Color.White;
            this.mrGroup2.BorderThickness = 1F;
            this.mrGroup2.Controls.Add(this.RChkArea);
            this.mrGroup2.Controls.Add(this.RChkAgent);
            this.mrGroup2.Controls.Add(this.RChkAccountSubGroup);
            this.mrGroup2.Controls.Add(this.RChkAccountGroup);
            this.mrGroup2.Controls.Add(this.RChkNormal);
            this.mrGroup2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup2.GroupImage = null;
            this.mrGroup2.GroupTitle = "Report Type";
            this.mrGroup2.Location = new System.Drawing.Point(3, 72);
            this.mrGroup2.Name = "mrGroup2";
            this.mrGroup2.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup2.PaintGroupBox = false;
            this.mrGroup2.RoundCorners = 10;
            this.mrGroup2.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup2.ShadowControl = false;
            this.mrGroup2.ShadowThickness = 3;
            this.mrGroup2.Size = new System.Drawing.Size(241, 164);
            this.mrGroup2.TabIndex = 1;
            // 
            // RChkArea
            // 
            this.RChkArea.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.RChkArea.Location = new System.Drawing.Point(10, 131);
            this.RChkArea.Name = "RChkArea";
            this.RChkArea.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RChkArea.Size = new System.Drawing.Size(170, 25);
            this.RChkArea.TabIndex = 4;
            this.RChkArea.Text = "Area";
            this.RChkArea.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.RChkArea.UseVisualStyleBackColor = true;
            // 
            // RChkAgent
            // 
            this.RChkAgent.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.RChkAgent.Location = new System.Drawing.Point(10, 106);
            this.RChkAgent.Name = "RChkAgent";
            this.RChkAgent.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RChkAgent.Size = new System.Drawing.Size(170, 25);
            this.RChkAgent.TabIndex = 3;
            this.RChkAgent.Text = "Agent";
            this.RChkAgent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.RChkAgent.UseVisualStyleBackColor = true;
            // 
            // RChkAccountSubGroup
            // 
            this.RChkAccountSubGroup.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.RChkAccountSubGroup.Location = new System.Drawing.Point(10, 81);
            this.RChkAccountSubGroup.Name = "RChkAccountSubGroup";
            this.RChkAccountSubGroup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RChkAccountSubGroup.Size = new System.Drawing.Size(170, 25);
            this.RChkAccountSubGroup.TabIndex = 2;
            this.RChkAccountSubGroup.Text = "Account Sub Group";
            this.RChkAccountSubGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.RChkAccountSubGroup.UseVisualStyleBackColor = true;
            // 
            // RChkAccountGroup
            // 
            this.RChkAccountGroup.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.RChkAccountGroup.Location = new System.Drawing.Point(10, 56);
            this.RChkAccountGroup.Name = "RChkAccountGroup";
            this.RChkAccountGroup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RChkAccountGroup.Size = new System.Drawing.Size(170, 25);
            this.RChkAccountGroup.TabIndex = 1;
            this.RChkAccountGroup.Text = "Account Group";
            this.RChkAccountGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.RChkAccountGroup.UseVisualStyleBackColor = true;
            // 
            // RChkNormal
            // 
            this.RChkNormal.Checked = true;
            this.RChkNormal.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.RChkNormal.Location = new System.Drawing.Point(10, 31);
            this.RChkNormal.Name = "RChkNormal";
            this.RChkNormal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RChkNormal.Size = new System.Drawing.Size(170, 25);
            this.RChkNormal.TabIndex = 0;
            this.RChkNormal.TabStop = true;
            this.RChkNormal.Text = "Normal";
            this.RChkNormal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.RChkNormal.UseVisualStyleBackColor = true;
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.CmbReportMode);
            this.mrGroup1.Controls.Add(this.lbl_Opening);
            this.mrGroup1.Controls.Add(this.lbl_AsOnDate);
            this.mrGroup1.Controls.Add(this.MskAsOnDate);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "Date Filter";
            this.mrGroup1.Location = new System.Drawing.Point(3, 6);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 2;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(546, 63);
            this.mrGroup1.TabIndex = 0;
            // 
            // mrGroup3
            // 
            this.mrGroup3.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup3.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup3.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup3.BorderColor = System.Drawing.Color.White;
            this.mrGroup3.BorderThickness = 1F;
            this.mrGroup3.Controls.Add(this.ChkAgentOnly);
            this.mrGroup3.Controls.Add(this.ChkDocumentAgent);
            this.mrGroup3.Controls.Add(this.ChkIncludeSales);
            this.mrGroup3.Controls.Add(this.ChkIncludeCreditLimit);
            this.mrGroup3.Controls.Add(this.ChkDate);
            this.mrGroup3.Controls.Add(this.clsSeparator3);
            this.mrGroup3.Controls.Add(this.ChkSelectAll);
            this.mrGroup3.Controls.Add(this.BtnShow);
            this.mrGroup3.Controls.Add(this.BtnCancel);
            this.mrGroup3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup3.GroupImage = null;
            this.mrGroup3.GroupTitle = "";
            this.mrGroup3.Location = new System.Drawing.Point(3, 227);
            this.mrGroup3.Name = "mrGroup3";
            this.mrGroup3.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup3.PaintGroupBox = false;
            this.mrGroup3.RoundCorners = 4;
            this.mrGroup3.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup3.ShadowControl = false;
            this.mrGroup3.ShadowThickness = 3;
            this.mrGroup3.Size = new System.Drawing.Size(546, 115);
            this.mrGroup3.TabIndex = 3;
            // 
            // FrmAgingReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(554, 345);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmAgingReport";
            this.ShowIcon = false;
            this.Text = "AGING REPORT";
            this.Load += new System.EventHandler(this.FrmAgingReport_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmAgingReport_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            this.mrGroup4.ResumeLayout(false);
            this.mrGroup4.PerformLayout();
            this.mrGroup2.ResumeLayout(false);
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.mrGroup3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_Slab;
        private MrTextBox TxtAgeingSlabDays;
        private System.Windows.Forms.CheckBox ChkDocumentAgent;
        private System.Windows.Forms.CheckBox ChkDate;
        private System.Windows.Forms.CheckBox ChkSelectAll;
        private System.Windows.Forms.CheckBox ChkAgentOnly;
        private System.Windows.Forms.Label lbl_AsOnDate;
        private MrMaskedTextBox MskAsOnDate;
        private System.Windows.Forms.Label lbl_Opening;
        private System.Windows.Forms.ComboBox CmbReportMode;
        private System.Windows.Forms.Label Lbl_On;
        private System.Windows.Forms.ComboBox CmbAgingOn;
        private System.Windows.Forms.Label lbl_Columns;
        private System.Windows.Forms.ComboBox CmbColumnNumber;
        private System.Windows.Forms.ComboBox CmbAgingSlab;
        private System.Windows.Forms.Label lbl_SlabType;
        private System.Windows.Forms.CheckBox ChkIncludeCreditLimit;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private System.Windows.Forms.CheckBox ChkIncludeSales;
        private ClsSeparator clsSeparator3;
        private MrPanel PanelHeader;
        private MrGroup mrGroup1;
        private MrGroup mrGroup3;
        private MrGroup mrGroup2;
        private System.Windows.Forms.RadioButton RChkNormal;
        private System.Windows.Forms.RadioButton RChkAgent;
        private System.Windows.Forms.RadioButton RChkAccountSubGroup;
        private System.Windows.Forms.RadioButton RChkAccountGroup;
        private System.Windows.Forms.RadioButton RChkArea;
        private MrGroup mrGroup4;
    }
}