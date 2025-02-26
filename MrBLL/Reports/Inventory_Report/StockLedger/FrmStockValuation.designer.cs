using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Inventory_Report.StockLedger
{
    partial class FrmStockValuation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStockValuation));
            this.PanelHeader = new MrPanel();
            this.mrGroup3 = new MrGroup();
            this.ChkIncludeProduct = new System.Windows.Forms.CheckBox();
            this.ChkShortName = new System.Windows.Forms.CheckBox();
            this.clsSeparator1 = new ClsSeparator();
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.ChkDynamicReports = new System.Windows.Forms.CheckBox();
            this.ChkRepostValue = new System.Windows.Forms.CheckBox();
            this.ChkIncludeVat = new System.Windows.Forms.CheckBox();
            this.ChkSelectAll = new System.Windows.Forms.CheckBox();
            this.ChkDate = new System.Windows.Forms.CheckBox();
            this.ChkIncludeAltQty = new System.Windows.Forms.CheckBox();
            this.ChkIncludeBatch = new System.Windows.Forms.CheckBox();
            this.mrGroup2 = new MrGroup();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rChkInclude = new System.Windows.Forms.RadioButton();
            this.rChkOnly = new System.Windows.Forms.RadioButton();
            this.rChkExclude = new System.Windows.Forms.RadioButton();
            this.rChkGodown = new System.Windows.Forms.RadioButton();
            this.GrpReportType = new System.Windows.Forms.GroupBox();
            this.rChkAmount = new System.Windows.Forms.RadioButton();
            this.rChkDescription = new System.Windows.Forms.RadioButton();
            this.rChkQuantity = new System.Windows.Forms.RadioButton();
            this.rChkShortName = new System.Windows.Forms.RadioButton();
            this.rChkProduct = new System.Windows.Forms.RadioButton();
            this.rChkSubGroup = new System.Windows.Forms.RadioButton();
            this.rChkGroup = new System.Windows.Forms.RadioButton();
            this.mrGroup1 = new MrGroup();
            this.rChkWeightedAverage = new System.Windows.Forms.RadioButton();
            this.MskFrom = new MrMaskedTextBox();
            this.rChkFifoMethod = new System.Windows.Forms.RadioButton();
            this.MskToDate = new MrMaskedTextBox();
            this.CmbDateType = new System.Windows.Forms.ComboBox();
            this.PanelHeader.SuspendLayout();
            this.mrGroup3.SuspendLayout();
            this.mrGroup2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.GrpReportType.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            this.SuspendLayout();
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
            this.PanelHeader.Size = new System.Drawing.Size(540, 372);
            this.PanelHeader.TabIndex = 0;
            // 
            // mrGroup3
            // 
            this.mrGroup3.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup3.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup3.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup3.BorderColor = System.Drawing.Color.White;
            this.mrGroup3.BorderThickness = 1F;
            this.mrGroup3.Controls.Add(this.ChkIncludeProduct);
            this.mrGroup3.Controls.Add(this.ChkShortName);
            this.mrGroup3.Controls.Add(this.clsSeparator1);
            this.mrGroup3.Controls.Add(this.BtnShow);
            this.mrGroup3.Controls.Add(this.BtnCancel);
            this.mrGroup3.Controls.Add(this.ChkDynamicReports);
            this.mrGroup3.Controls.Add(this.ChkRepostValue);
            this.mrGroup3.Controls.Add(this.ChkIncludeVat);
            this.mrGroup3.Controls.Add(this.ChkSelectAll);
            this.mrGroup3.Controls.Add(this.ChkDate);
            this.mrGroup3.Controls.Add(this.ChkIncludeAltQty);
            this.mrGroup3.Controls.Add(this.ChkIncludeBatch);
            this.mrGroup3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup3.GroupImage = null;
            this.mrGroup3.GroupTitle = "Filter Value";
            this.mrGroup3.Location = new System.Drawing.Point(2, 233);
            this.mrGroup3.Name = "mrGroup3";
            this.mrGroup3.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup3.PaintGroupBox = false;
            this.mrGroup3.RoundCorners = 10;
            this.mrGroup3.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup3.ShadowControl = false;
            this.mrGroup3.ShadowThickness = 3;
            this.mrGroup3.Size = new System.Drawing.Size(510, 136);
            this.mrGroup3.TabIndex = 3;
            // 
            // ChkIncludeProduct
            // 
            this.ChkIncludeProduct.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeProduct.Location = new System.Drawing.Point(11, 46);
            this.ChkIncludeProduct.Margin = new System.Windows.Forms.Padding(5);
            this.ChkIncludeProduct.Name = "ChkIncludeProduct";
            this.ChkIncludeProduct.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeProduct.Size = new System.Drawing.Size(153, 26);
            this.ChkIncludeProduct.TabIndex = 2;
            this.ChkIncludeProduct.Text = "Include Product";
            this.ChkIncludeProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeProduct.UseVisualStyleBackColor = true;
            // 
            // ChkShortName
            // 
            this.ChkShortName.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkShortName.Location = new System.Drawing.Point(308, 71);
            this.ChkShortName.Name = "ChkShortName";
            this.ChkShortName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkShortName.Size = new System.Drawing.Size(153, 23);
            this.ChkShortName.TabIndex = 14;
            this.ChkShortName.Text = "ShortName";
            this.ChkShortName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkShortName.UseVisualStyleBackColor = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator1.Location = new System.Drawing.Point(3, 97);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(507, 2);
            this.clsSeparator1.TabIndex = 13;
            this.clsSeparator1.TabStop = false;
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnShow.Location = new System.Drawing.Point(265, 99);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(100, 34);
            this.BtnShow.TabIndex = 0;
            this.BtnShow.Text = "&SHOW";
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(368, 99);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(109, 34);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // ChkDynamicReports
            // 
            this.ChkDynamicReports.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDynamicReports.Location = new System.Drawing.Point(172, 70);
            this.ChkDynamicReports.Name = "ChkDynamicReports";
            this.ChkDynamicReports.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDynamicReports.Size = new System.Drawing.Size(126, 24);
            this.ChkDynamicReports.TabIndex = 12;
            this.ChkDynamicReports.Text = "Advance ";
            this.ChkDynamicReports.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDynamicReports.UseVisualStyleBackColor = false;
            // 
            // ChkRepostValue
            // 
            this.ChkRepostValue.Checked = true;
            this.ChkRepostValue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkRepostValue.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkRepostValue.Location = new System.Drawing.Point(11, 26);
            this.ChkRepostValue.Margin = new System.Windows.Forms.Padding(5);
            this.ChkRepostValue.Name = "ChkRepostValue";
            this.ChkRepostValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkRepostValue.Size = new System.Drawing.Size(153, 23);
            this.ChkRepostValue.TabIndex = 0;
            this.ChkRepostValue.Text = "Report Value";
            this.ChkRepostValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkRepostValue.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeVat
            // 
            this.ChkIncludeVat.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeVat.Location = new System.Drawing.Point(308, 48);
            this.ChkIncludeVat.Margin = new System.Windows.Forms.Padding(5);
            this.ChkIncludeVat.Name = "ChkIncludeVat";
            this.ChkIncludeVat.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeVat.Size = new System.Drawing.Size(153, 23);
            this.ChkIncludeVat.TabIndex = 6;
            this.ChkIncludeVat.Text = "With VAT";
            this.ChkIncludeVat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeVat.UseVisualStyleBackColor = true;
            // 
            // ChkSelectAll
            // 
            this.ChkSelectAll.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSelectAll.Location = new System.Drawing.Point(11, 69);
            this.ChkSelectAll.Margin = new System.Windows.Forms.Padding(5);
            this.ChkSelectAll.Name = "ChkSelectAll";
            this.ChkSelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSelectAll.Size = new System.Drawing.Size(153, 26);
            this.ChkSelectAll.TabIndex = 3;
            this.ChkSelectAll.Text = "Select All";
            this.ChkSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSelectAll.UseVisualStyleBackColor = true;
            // 
            // IsDate
            // 
            this.ChkDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDate.Location = new System.Drawing.Point(172, 26);
            this.ChkDate.Margin = new System.Windows.Forms.Padding(5);
            this.ChkDate.Name = "ChkDate";
            this.ChkDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDate.Size = new System.Drawing.Size(126, 23);
            this.ChkDate.TabIndex = 4;
            this.ChkDate.Text = "Date";
            this.ChkDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDate.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeAltQty
            // 
            this.ChkIncludeAltQty.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeAltQty.Location = new System.Drawing.Point(308, 26);
            this.ChkIncludeAltQty.Margin = new System.Windows.Forms.Padding(5);
            this.ChkIncludeAltQty.Name = "ChkIncludeAltQty";
            this.ChkIncludeAltQty.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeAltQty.Size = new System.Drawing.Size(153, 23);
            this.ChkIncludeAltQty.TabIndex = 1;
            this.ChkIncludeAltQty.Text = "Alt Quantity";
            this.ChkIncludeAltQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeAltQty.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeBatch
            // 
            this.ChkIncludeBatch.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeBatch.Location = new System.Drawing.Point(172, 48);
            this.ChkIncludeBatch.Margin = new System.Windows.Forms.Padding(5);
            this.ChkIncludeBatch.Name = "ChkIncludeBatch";
            this.ChkIncludeBatch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeBatch.Size = new System.Drawing.Size(126, 23);
            this.ChkIncludeBatch.TabIndex = 5;
            this.ChkIncludeBatch.Text = "Batch";
            this.ChkIncludeBatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeBatch.UseVisualStyleBackColor = true;
            // 
            // mrGroup2
            // 
            this.mrGroup2.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup2.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup2.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup2.BorderColor = System.Drawing.Color.White;
            this.mrGroup2.BorderThickness = 1F;
            this.mrGroup2.Controls.Add(this.groupBox1);
            this.mrGroup2.Controls.Add(this.rChkGodown);
            this.mrGroup2.Controls.Add(this.GrpReportType);
            this.mrGroup2.Controls.Add(this.rChkProduct);
            this.mrGroup2.Controls.Add(this.rChkSubGroup);
            this.mrGroup2.Controls.Add(this.rChkGroup);
            this.mrGroup2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup2.GroupImage = null;
            this.mrGroup2.GroupTitle = "Report Type";
            this.mrGroup2.Location = new System.Drawing.Point(3, 90);
            this.mrGroup2.Name = "mrGroup2";
            this.mrGroup2.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup2.PaintGroupBox = false;
            this.mrGroup2.RoundCorners = 10;
            this.mrGroup2.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup2.ShadowControl = false;
            this.mrGroup2.ShadowThickness = 3;
            this.mrGroup2.Size = new System.Drawing.Size(510, 141);
            this.mrGroup2.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rChkInclude);
            this.groupBox1.Controls.Add(this.rChkOnly);
            this.groupBox1.Controls.Add(this.rChkExclude);
            this.groupBox1.Location = new System.Drawing.Point(334, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(168, 124);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Negative Stock";
            // 
            // rChkInclude
            // 
            this.rChkInclude.AutoSize = true;
            this.rChkInclude.Checked = true;
            this.rChkInclude.Location = new System.Drawing.Point(6, 24);
            this.rChkInclude.Name = "rChkInclude";
            this.rChkInclude.Size = new System.Drawing.Size(86, 24);
            this.rChkInclude.TabIndex = 0;
            this.rChkInclude.TabStop = true;
            this.rChkInclude.Text = "Include";
            this.rChkInclude.UseVisualStyleBackColor = true;
            // 
            // rChkOnly
            // 
            this.rChkOnly.AutoSize = true;
            this.rChkOnly.Location = new System.Drawing.Point(6, 72);
            this.rChkOnly.Name = "rChkOnly";
            this.rChkOnly.Size = new System.Drawing.Size(65, 24);
            this.rChkOnly.TabIndex = 2;
            this.rChkOnly.Text = "Only";
            this.rChkOnly.UseVisualStyleBackColor = true;
            // 
            // rChkExclude
            // 
            this.rChkExclude.AutoSize = true;
            this.rChkExclude.Location = new System.Drawing.Point(6, 48);
            this.rChkExclude.Name = "rChkExclude";
            this.rChkExclude.Size = new System.Drawing.Size(90, 24);
            this.rChkExclude.TabIndex = 1;
            this.rChkExclude.Text = "Exclude";
            this.rChkExclude.UseVisualStyleBackColor = true;
            // 
            // rChkGodown
            // 
            this.rChkGodown.AutoSize = true;
            this.rChkGodown.Location = new System.Drawing.Point(23, 112);
            this.rChkGodown.Name = "rChkGodown";
            this.rChkGodown.Size = new System.Drawing.Size(91, 24);
            this.rChkGodown.TabIndex = 3;
            this.rChkGodown.Text = "Godown";
            this.rChkGodown.UseVisualStyleBackColor = true;
            // 
            // GrpReportType
            // 
            this.GrpReportType.Controls.Add(this.rChkAmount);
            this.GrpReportType.Controls.Add(this.rChkDescription);
            this.GrpReportType.Controls.Add(this.rChkQuantity);
            this.GrpReportType.Controls.Add(this.rChkShortName);
            this.GrpReportType.Location = new System.Drawing.Point(204, 15);
            this.GrpReportType.Name = "GrpReportType";
            this.GrpReportType.Size = new System.Drawing.Size(127, 124);
            this.GrpReportType.TabIndex = 2;
            this.GrpReportType.TabStop = false;
            this.GrpReportType.Text = "Sort On";
            // 
            // rChkAmount
            // 
            this.rChkAmount.AutoSize = true;
            this.rChkAmount.Location = new System.Drawing.Point(6, 96);
            this.rChkAmount.Name = "rChkAmount";
            this.rChkAmount.Size = new System.Drawing.Size(90, 24);
            this.rChkAmount.TabIndex = 3;
            this.rChkAmount.Text = "Amount";
            this.rChkAmount.UseVisualStyleBackColor = true;
            // 
            // rChkDescription
            // 
            this.rChkDescription.AutoSize = true;
            this.rChkDescription.Checked = true;
            this.rChkDescription.Location = new System.Drawing.Point(6, 24);
            this.rChkDescription.Name = "rChkDescription";
            this.rChkDescription.Size = new System.Drawing.Size(118, 24);
            this.rChkDescription.TabIndex = 0;
            this.rChkDescription.TabStop = true;
            this.rChkDescription.Text = "Description";
            this.rChkDescription.UseVisualStyleBackColor = true;
            // 
            // rChkQuantity
            // 
            this.rChkQuantity.AutoSize = true;
            this.rChkQuantity.Location = new System.Drawing.Point(6, 72);
            this.rChkQuantity.Name = "rChkQuantity";
            this.rChkQuantity.Size = new System.Drawing.Size(97, 24);
            this.rChkQuantity.TabIndex = 2;
            this.rChkQuantity.Text = "Quantity";
            this.rChkQuantity.UseVisualStyleBackColor = true;
            // 
            // rChkShortName
            // 
            this.rChkShortName.AutoSize = true;
            this.rChkShortName.Location = new System.Drawing.Point(6, 48);
            this.rChkShortName.Name = "rChkShortName";
            this.rChkShortName.Size = new System.Drawing.Size(116, 24);
            this.rChkShortName.TabIndex = 1;
            this.rChkShortName.Text = "ShortName";
            this.rChkShortName.UseVisualStyleBackColor = true;
            // 
            // rChkProduct
            // 
            this.rChkProduct.AutoSize = true;
            this.rChkProduct.Checked = true;
            this.rChkProduct.Location = new System.Drawing.Point(23, 29);
            this.rChkProduct.Name = "rChkProduct";
            this.rChkProduct.Size = new System.Drawing.Size(88, 24);
            this.rChkProduct.TabIndex = 0;
            this.rChkProduct.TabStop = true;
            this.rChkProduct.Text = "Product";
            this.rChkProduct.UseVisualStyleBackColor = true;
            this.rChkProduct.CheckedChanged += new System.EventHandler(this.rChkProduct_CheckedChanged);
            // 
            // rChkSubGroup
            // 
            this.rChkSubGroup.AutoSize = true;
            this.rChkSubGroup.Location = new System.Drawing.Point(23, 83);
            this.rChkSubGroup.Name = "rChkSubGroup";
            this.rChkSubGroup.Size = new System.Drawing.Size(175, 24);
            this.rChkSubGroup.TabIndex = 2;
            this.rChkSubGroup.Text = "Product SubGroup";
            this.rChkSubGroup.UseVisualStyleBackColor = true;
            // 
            // rChkGroup
            // 
            this.rChkGroup.AutoSize = true;
            this.rChkGroup.Location = new System.Drawing.Point(23, 56);
            this.rChkGroup.Name = "rChkGroup";
            this.rChkGroup.Size = new System.Drawing.Size(143, 24);
            this.rChkGroup.TabIndex = 1;
            this.rChkGroup.Text = "Product Group";
            this.rChkGroup.UseVisualStyleBackColor = true;
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.rChkWeightedAverage);
            this.mrGroup1.Controls.Add(this.MskFrom);
            this.mrGroup1.Controls.Add(this.rChkFifoMethod);
            this.mrGroup1.Controls.Add(this.MskToDate);
            this.mrGroup1.Controls.Add(this.CmbDateType);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "Date Filter";
            this.mrGroup1.Location = new System.Drawing.Point(3, 0);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 10;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(510, 89);
            this.mrGroup1.TabIndex = 1;
            // 
            // rChkWeightedAverage
            // 
            this.rChkWeightedAverage.AutoSize = true;
            this.rChkWeightedAverage.Location = new System.Drawing.Point(387, 29);
            this.rChkWeightedAverage.Name = "rChkWeightedAverage";
            this.rChkWeightedAverage.Size = new System.Drawing.Size(100, 24);
            this.rChkWeightedAverage.TabIndex = 1;
            this.rChkWeightedAverage.Text = "WG. AVG";
            this.rChkWeightedAverage.UseVisualStyleBackColor = true;
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskFrom.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskFrom.Location = new System.Drawing.Point(8, 58);
            this.MskFrom.Margin = new System.Windows.Forms.Padding(5);
            this.MskFrom.Mask = "00/00/0000";
            this.MskFrom.Name = "MskFrom";
            this.MskFrom.Size = new System.Drawing.Size(127, 26);
            this.MskFrom.TabIndex = 1;
            this.MskFrom.Enter += new System.EventHandler(this.MskFrom_Enter);
            this.MskFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MskFrom_KeyDown);
            this.MskFrom.Leave += new System.EventHandler(this.MskFrom_Leave);
            this.MskFrom.Validated += new System.EventHandler(this.MskFrom_Validated);
            // 
            // rChkFifoMethod
            // 
            this.rChkFifoMethod.AutoSize = true;
            this.rChkFifoMethod.Checked = true;
            this.rChkFifoMethod.Location = new System.Drawing.Point(387, 59);
            this.rChkFifoMethod.Name = "rChkFifoMethod";
            this.rChkFifoMethod.Size = new System.Drawing.Size(65, 24);
            this.rChkFifoMethod.TabIndex = 0;
            this.rChkFifoMethod.TabStop = true;
            this.rChkFifoMethod.Text = "FIFO";
            this.rChkFifoMethod.UseVisualStyleBackColor = true;
            // 
            // MskToDate
            // 
            this.MskToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskToDate.Location = new System.Drawing.Point(136, 58);
            this.MskToDate.Margin = new System.Windows.Forms.Padding(5);
            this.MskToDate.Mask = "00/00/0000";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.Size = new System.Drawing.Size(127, 26);
            this.MskToDate.TabIndex = 2;
            this.MskToDate.Enter += new System.EventHandler(this.MskToDate_Enter);
            this.MskToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MskToDate_KeyDown);
            this.MskToDate.Leave += new System.EventHandler(this.MskToDate_Leave);
            this.MskToDate.Validated += new System.EventHandler(this.MskToDate_Validated);
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
            this.CmbDateType.Location = new System.Drawing.Point(9, 27);
            this.CmbDateType.Name = "CmbDateType";
            this.CmbDateType.Size = new System.Drawing.Size(343, 28);
            this.CmbDateType.TabIndex = 0;
            this.CmbDateType.SelectedIndexChanged += new System.EventHandler(this.CmbDateType_SelectedIndexChanged);
            this.CmbDateType.Enter += new System.EventHandler(this.CmbDateType_Enter);
            this.CmbDateType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbDateType_KeyDown);
            this.CmbDateType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbDateType_KeyPress);
            this.CmbDateType.Leave += new System.EventHandler(this.CmbDateType_Leave);
            // 
            // FrmStockValuation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(514, 372);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "FrmStockValuation";
            this.ShowIcon = false;
            this.Text = "Stock Valuation";
            this.Load += new System.EventHandler(this.FrmStockValuation_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmStockValuation_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            this.mrGroup3.ResumeLayout(false);
            this.mrGroup2.ResumeLayout(false);
            this.mrGroup2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.GrpReportType.ResumeLayout(false);
            this.GrpReportType.PerformLayout();
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox ChkIncludeBatch;
        private System.Windows.Forms.CheckBox ChkDate;
        private System.Windows.Forms.CheckBox ChkSelectAll;
        private System.Windows.Forms.CheckBox ChkIncludeAltQty;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private System.Windows.Forms.ComboBox CmbDateType;
        private System.Windows.Forms.CheckBox ChkRepostValue;
        private System.Windows.Forms.RadioButton rChkWeightedAverage;
        private System.Windows.Forms.RadioButton rChkFifoMethod;
        private System.Windows.Forms.GroupBox GrpReportType;
        private System.Windows.Forms.RadioButton rChkProduct;
        private System.Windows.Forms.RadioButton rChkGroup;
        private System.Windows.Forms.RadioButton rChkSubGroup;
        private System.Windows.Forms.RadioButton rChkGodown;
        private System.Windows.Forms.CheckBox ChkIncludeProduct;
        private System.Windows.Forms.CheckBox ChkIncludeVat;
        private MrGroup mrGroup2;
        private MrGroup mrGroup1;
        private MrGroup mrGroup3;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.CheckBox ChkDynamicReports;
        private System.Windows.Forms.RadioButton rChkDescription;
        private System.Windows.Forms.RadioButton rChkQuantity;
        private System.Windows.Forms.RadioButton rChkShortName;
        private System.Windows.Forms.RadioButton rChkAmount;
        private System.Windows.Forms.CheckBox ChkShortName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rChkInclude;
        private System.Windows.Forms.RadioButton rChkOnly;
        private System.Windows.Forms.RadioButton rChkExclude;
        private MrMaskedTextBox MskToDate;
        private MrMaskedTextBox MskFrom;
        private MrPanel PanelHeader;
    }
}