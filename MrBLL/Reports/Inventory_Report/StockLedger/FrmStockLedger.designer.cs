using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Inventory_Report.StockLedger
{
    partial class FrmStockLedger
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
            this.PanelHeader = new MrPanel();
            this.mrGroup5 = new MrGroup();
            this.ChkIncludeVat = new System.Windows.Forms.CheckBox();
            this.ChkSummary = new System.Windows.Forms.CheckBox();
            this.clsSeparator1 = new ClsSeparator();
            this.ChkWithValue = new System.Windows.Forms.CheckBox();
            this.ChkOnly = new System.Windows.Forms.CheckBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.ChkAltQuantity = new System.Windows.Forms.CheckBox();
            this.ChkDate = new System.Windows.Forms.CheckBox();
            this.ChkFIFO = new System.Windows.Forms.CheckBox();
            this.ChkBatchWise = new System.Windows.Forms.CheckBox();
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.ChkDynamicReports = new System.Windows.Forms.CheckBox();
            this.ChkZeroBalance = new System.Windows.Forms.CheckBox();
            this.ChkSelectAll = new System.Windows.Forms.CheckBox();
            this.mrGroup4 = new MrGroup();
            this.RbtnQuantity = new System.Windows.Forms.RadioButton();
            this.RbtnDescrption = new System.Windows.Forms.RadioButton();
            this.RbtnBarcode = new System.Windows.Forms.RadioButton();
            this.mrGroup3 = new MrGroup();
            this.rChkNegativeOnly = new System.Windows.Forms.RadioButton();
            this.rChkNegativeInclude = new System.Windows.Forms.RadioButton();
            this.rChkNegativeExclude = new System.Windows.Forms.RadioButton();
            this.mrGroup2 = new MrGroup();
            this.rChkSubGroupWise = new System.Windows.Forms.RadioButton();
            this.rChkProductWise = new System.Windows.Forms.RadioButton();
            this.rChkGroupWise = new System.Windows.Forms.RadioButton();
            this.mrGroup1 = new MrGroup();
            this.MskToDate = new MrMaskedTextBox();
            this.CmbDateType = new System.Windows.Forms.ComboBox();
            this.MskFrom = new MrMaskedTextBox();
            this.ChkRePostValue = new System.Windows.Forms.CheckBox();
            this.PanelHeader.SuspendLayout();
            this.mrGroup5.SuspendLayout();
            this.mrGroup4.SuspendLayout();
            this.mrGroup3.SuspendLayout();
            this.mrGroup2.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.mrGroup5);
            this.PanelHeader.Controls.Add(this.mrGroup4);
            this.PanelHeader.Controls.Add(this.mrGroup3);
            this.PanelHeader.Controls.Add(this.mrGroup2);
            this.PanelHeader.Controls.Add(this.mrGroup1);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(571, 346);
            this.PanelHeader.TabIndex = 0;
            // 
            // mrGroup5
            // 
            this.mrGroup5.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup5.BackgroundGradientColor = System.Drawing.Color.WhiteSmoke;
            this.mrGroup5.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup5.BorderColor = System.Drawing.Color.White;
            this.mrGroup5.BorderThickness = 1F;
            this.mrGroup5.Controls.Add(this.ChkRePostValue);
            this.mrGroup5.Controls.Add(this.ChkIncludeVat);
            this.mrGroup5.Controls.Add(this.ChkSummary);
            this.mrGroup5.Controls.Add(this.clsSeparator1);
            this.mrGroup5.Controls.Add(this.ChkWithValue);
            this.mrGroup5.Controls.Add(this.ChkOnly);
            this.mrGroup5.Controls.Add(this.BtnCancel);
            this.mrGroup5.Controls.Add(this.ChkAltQuantity);
            this.mrGroup5.Controls.Add(this.ChkDate);
            this.mrGroup5.Controls.Add(this.ChkFIFO);
            this.mrGroup5.Controls.Add(this.ChkBatchWise);
            this.mrGroup5.Controls.Add(this.BtnShow);
            this.mrGroup5.Controls.Add(this.ChkDynamicReports);
            this.mrGroup5.Controls.Add(this.ChkZeroBalance);
            this.mrGroup5.Controls.Add(this.ChkSelectAll);
            this.mrGroup5.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup5.GroupImage = null;
            this.mrGroup5.GroupTitle = "Filter Value";
            this.mrGroup5.Location = new System.Drawing.Point(4, 174);
            this.mrGroup5.Name = "mrGroup5";
            this.mrGroup5.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup5.PaintGroupBox = false;
            this.mrGroup5.RoundCorners = 10;
            this.mrGroup5.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup5.ShadowControl = false;
            this.mrGroup5.ShadowThickness = 3;
            this.mrGroup5.Size = new System.Drawing.Size(558, 167);
            this.mrGroup5.TabIndex = 4;
            // 
            // ChkIncludeVat
            // 
            this.ChkIncludeVat.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeVat.Location = new System.Drawing.Point(178, 95);
            this.ChkIncludeVat.Name = "ChkIncludeVat";
            this.ChkIncludeVat.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeVat.Size = new System.Drawing.Size(163, 24);
            this.ChkIncludeVat.TabIndex = 8;
            this.ChkIncludeVat.Text = "With VAT";
            this.ChkIncludeVat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeVat.UseVisualStyleBackColor = true;
            // 
            // ChkSummary
            // 
            this.ChkSummary.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSummary.Location = new System.Drawing.Point(5, 23);
            this.ChkSummary.Name = "ChkSummary";
            this.ChkSummary.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSummary.Size = new System.Drawing.Size(163, 24);
            this.ChkSummary.TabIndex = 1;
            this.ChkSummary.Text = "Summary";
            this.ChkSummary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSummary.UseVisualStyleBackColor = true;
            this.ChkSummary.CheckedChanged += new System.EventHandler(this.ChkSummary_CheckedChanged);
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.Wheat;
            this.clsSeparator1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clsSeparator1.Location = new System.Drawing.Point(5, 123);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(547, 2);
            this.clsSeparator1.TabIndex = 77;
            this.clsSeparator1.TabStop = false;
            // 
            // ChkWithValue
            // 
            this.ChkWithValue.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkWithValue.Location = new System.Drawing.Point(5, 47);
            this.ChkWithValue.Name = "ChkWithValue";
            this.ChkWithValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkWithValue.Size = new System.Drawing.Size(163, 24);
            this.ChkWithValue.TabIndex = 2;
            this.ChkWithValue.Text = "With Value";
            this.ChkWithValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkWithValue.UseVisualStyleBackColor = true;
            // 
            // ChkOnly
            // 
            this.ChkOnly.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkOnly.Location = new System.Drawing.Point(178, 71);
            this.ChkOnly.Name = "ChkOnly";
            this.ChkOnly.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkOnly.Size = new System.Drawing.Size(163, 24);
            this.ChkOnly.TabIndex = 7;
            this.ChkOnly.Text = "Only";
            this.ChkOnly.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkOnly.UseVisualStyleBackColor = true;
            this.ChkOnly.CheckedChanged += new System.EventHandler(this.ChkOnly_CheckedChanged);
            this.ChkOnly.Click += new System.EventHandler(this.ChkOnly_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(304, 127);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(111, 35);
            this.BtnCancel.TabIndex = 13;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // ChkAltQuantity
            // 
            this.ChkAltQuantity.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAltQuantity.Location = new System.Drawing.Point(347, 47);
            this.ChkAltQuantity.Name = "ChkAltQuantity";
            this.ChkAltQuantity.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkAltQuantity.Size = new System.Drawing.Size(163, 24);
            this.ChkAltQuantity.TabIndex = 10;
            this.ChkAltQuantity.Text = "Alt Quantity";
            this.ChkAltQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkAltQuantity.UseVisualStyleBackColor = true;
            // 
            // IsDate
            // 
            this.ChkDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDate.Location = new System.Drawing.Point(178, 23);
            this.ChkDate.Name = "ChkDate";
            this.ChkDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDate.Size = new System.Drawing.Size(163, 24);
            this.ChkDate.TabIndex = 5;
            this.ChkDate.Text = "Date";
            this.ChkDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDate.UseVisualStyleBackColor = true;
            // 
            // ChkFIFO
            // 
            this.ChkFIFO.Checked = true;
            this.ChkFIFO.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkFIFO.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkFIFO.Location = new System.Drawing.Point(347, 23);
            this.ChkFIFO.Name = "ChkFIFO";
            this.ChkFIFO.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkFIFO.Size = new System.Drawing.Size(163, 24);
            this.ChkFIFO.TabIndex = 9;
            this.ChkFIFO.Text = "FIFO";
            this.ChkFIFO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkFIFO.UseVisualStyleBackColor = true;
            // 
            // ChkBatchWise
            // 
            this.ChkBatchWise.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkBatchWise.Location = new System.Drawing.Point(178, 47);
            this.ChkBatchWise.Name = "ChkBatchWise";
            this.ChkBatchWise.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkBatchWise.Size = new System.Drawing.Size(163, 24);
            this.ChkBatchWise.TabIndex = 6;
            this.ChkBatchWise.Text = "Batch";
            this.ChkBatchWise.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkBatchWise.UseVisualStyleBackColor = true;
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.BtnShow.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnShow.Location = new System.Drawing.Point(204, 127);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(98, 35);
            this.BtnShow.TabIndex = 12;
            this.BtnShow.Text = "&SHOW";
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // ChkDynamicReports
            // 
            this.ChkDynamicReports.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDynamicReports.Location = new System.Drawing.Point(347, 70);
            this.ChkDynamicReports.Name = "ChkDynamicReports";
            this.ChkDynamicReports.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDynamicReports.Size = new System.Drawing.Size(163, 24);
            this.ChkDynamicReports.TabIndex = 11;
            this.ChkDynamicReports.Text = "Advance";
            this.ChkDynamicReports.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDynamicReports.UseVisualStyleBackColor = true;
            this.ChkDynamicReports.CheckedChanged += new System.EventHandler(this.ChkDynamicReports_CheckedChanged);
            // 
            // ChkZeroBalance
            // 
            this.ChkZeroBalance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkZeroBalance.Location = new System.Drawing.Point(5, 71);
            this.ChkZeroBalance.Name = "ChkZeroBalance";
            this.ChkZeroBalance.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkZeroBalance.Size = new System.Drawing.Size(163, 24);
            this.ChkZeroBalance.TabIndex = 3;
            this.ChkZeroBalance.Text = "Zero Balance";
            this.ChkZeroBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkZeroBalance.UseVisualStyleBackColor = true;
            // 
            // ChkSelectAll
            // 
            this.ChkSelectAll.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSelectAll.Location = new System.Drawing.Point(5, 95);
            this.ChkSelectAll.Name = "ChkSelectAll";
            this.ChkSelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSelectAll.Size = new System.Drawing.Size(163, 24);
            this.ChkSelectAll.TabIndex = 4;
            this.ChkSelectAll.Text = "Select All";
            this.ChkSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSelectAll.UseVisualStyleBackColor = true;
            // 
            // mrGroup4
            // 
            this.mrGroup4.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup4.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup4.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup4.BorderColor = System.Drawing.Color.White;
            this.mrGroup4.BorderThickness = 1F;
            this.mrGroup4.Controls.Add(this.RbtnQuantity);
            this.mrGroup4.Controls.Add(this.RbtnDescrption);
            this.mrGroup4.Controls.Add(this.RbtnBarcode);
            this.mrGroup4.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup4.GroupImage = null;
            this.mrGroup4.GroupTitle = "Sort On";
            this.mrGroup4.Location = new System.Drawing.Point(388, 66);
            this.mrGroup4.Name = "mrGroup4";
            this.mrGroup4.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup4.PaintGroupBox = false;
            this.mrGroup4.RoundCorners = 10;
            this.mrGroup4.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup4.ShadowControl = false;
            this.mrGroup4.ShadowThickness = 3;
            this.mrGroup4.Size = new System.Drawing.Size(174, 106);
            this.mrGroup4.TabIndex = 3;
            // 
            // RbtnQuantity
            // 
            this.RbtnQuantity.AutoSize = true;
            this.RbtnQuantity.Location = new System.Drawing.Point(8, 75);
            this.RbtnQuantity.Name = "RbtnQuantity";
            this.RbtnQuantity.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RbtnQuantity.Size = new System.Drawing.Size(97, 24);
            this.RbtnQuantity.TabIndex = 2;
            this.RbtnQuantity.Text = "Quantity";
            this.RbtnQuantity.UseVisualStyleBackColor = true;
            // 
            // RbtnDescrption
            // 
            this.RbtnDescrption.AutoSize = true;
            this.RbtnDescrption.Checked = true;
            this.RbtnDescrption.Location = new System.Drawing.Point(8, 24);
            this.RbtnDescrption.Name = "RbtnDescrption";
            this.RbtnDescrption.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RbtnDescrption.Size = new System.Drawing.Size(72, 24);
            this.RbtnDescrption.TabIndex = 0;
            this.RbtnDescrption.TabStop = true;
            this.RbtnDescrption.Text = "Name";
            this.RbtnDescrption.UseVisualStyleBackColor = true;
            // 
            // RbtnBarcode
            // 
            this.RbtnBarcode.AutoSize = true;
            this.RbtnBarcode.Location = new System.Drawing.Point(8, 51);
            this.RbtnBarcode.Name = "RbtnBarcode";
            this.RbtnBarcode.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RbtnBarcode.Size = new System.Drawing.Size(95, 24);
            this.RbtnBarcode.TabIndex = 1;
            this.RbtnBarcode.Text = "BarCode";
            this.RbtnBarcode.UseVisualStyleBackColor = true;
            // 
            // mrGroup3
            // 
            this.mrGroup3.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup3.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup3.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup3.BorderColor = System.Drawing.Color.White;
            this.mrGroup3.BorderThickness = 1F;
            this.mrGroup3.Controls.Add(this.rChkNegativeOnly);
            this.mrGroup3.Controls.Add(this.rChkNegativeInclude);
            this.mrGroup3.Controls.Add(this.rChkNegativeExclude);
            this.mrGroup3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup3.GroupImage = null;
            this.mrGroup3.GroupTitle = "Negative";
            this.mrGroup3.Location = new System.Drawing.Point(256, 66);
            this.mrGroup3.Name = "mrGroup3";
            this.mrGroup3.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup3.PaintGroupBox = false;
            this.mrGroup3.RoundCorners = 10;
            this.mrGroup3.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup3.ShadowControl = false;
            this.mrGroup3.ShadowThickness = 3;
            this.mrGroup3.Size = new System.Drawing.Size(126, 106);
            this.mrGroup3.TabIndex = 2;
            // 
            // rChkNegativeOnly
            // 
            this.rChkNegativeOnly.AutoSize = true;
            this.rChkNegativeOnly.Location = new System.Drawing.Point(9, 75);
            this.rChkNegativeOnly.Name = "rChkNegativeOnly";
            this.rChkNegativeOnly.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rChkNegativeOnly.Size = new System.Drawing.Size(65, 24);
            this.rChkNegativeOnly.TabIndex = 2;
            this.rChkNegativeOnly.Text = "Only";
            this.rChkNegativeOnly.UseVisualStyleBackColor = true;
            // 
            // rChkNegativeInclude
            // 
            this.rChkNegativeInclude.AutoSize = true;
            this.rChkNegativeInclude.Checked = true;
            this.rChkNegativeInclude.Location = new System.Drawing.Point(9, 27);
            this.rChkNegativeInclude.Name = "rChkNegativeInclude";
            this.rChkNegativeInclude.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rChkNegativeInclude.Size = new System.Drawing.Size(86, 24);
            this.rChkNegativeInclude.TabIndex = 0;
            this.rChkNegativeInclude.TabStop = true;
            this.rChkNegativeInclude.Text = "Include";
            this.rChkNegativeInclude.UseVisualStyleBackColor = true;
            // 
            // rChkNegativeExclude
            // 
            this.rChkNegativeExclude.AutoSize = true;
            this.rChkNegativeExclude.Location = new System.Drawing.Point(9, 51);
            this.rChkNegativeExclude.Name = "rChkNegativeExclude";
            this.rChkNegativeExclude.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rChkNegativeExclude.Size = new System.Drawing.Size(90, 24);
            this.rChkNegativeExclude.TabIndex = 1;
            this.rChkNegativeExclude.Text = "Exclude";
            this.rChkNegativeExclude.UseVisualStyleBackColor = true;
            // 
            // mrGroup2
            // 
            this.mrGroup2.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup2.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup2.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup2.BorderColor = System.Drawing.Color.White;
            this.mrGroup2.BorderThickness = 1F;
            this.mrGroup2.Controls.Add(this.rChkSubGroupWise);
            this.mrGroup2.Controls.Add(this.rChkProductWise);
            this.mrGroup2.Controls.Add(this.rChkGroupWise);
            this.mrGroup2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup2.GroupImage = null;
            this.mrGroup2.GroupTitle = "MrGroup";
            this.mrGroup2.Location = new System.Drawing.Point(4, 66);
            this.mrGroup2.Name = "mrGroup2";
            this.mrGroup2.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup2.PaintGroupBox = false;
            this.mrGroup2.RoundCorners = 10;
            this.mrGroup2.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup2.ShadowControl = false;
            this.mrGroup2.ShadowThickness = 3;
            this.mrGroup2.Size = new System.Drawing.Size(246, 106);
            this.mrGroup2.TabIndex = 1;
            // 
            // rChkSubGroupWise
            // 
            this.rChkSubGroupWise.AutoSize = true;
            this.rChkSubGroupWise.Location = new System.Drawing.Point(8, 76);
            this.rChkSubGroupWise.Name = "rChkSubGroupWise";
            this.rChkSubGroupWise.Size = new System.Drawing.Size(175, 24);
            this.rChkSubGroupWise.TabIndex = 2;
            this.rChkSubGroupWise.Text = "Product SubGroup";
            this.rChkSubGroupWise.UseVisualStyleBackColor = true;
            this.rChkSubGroupWise.CheckedChanged += new System.EventHandler(this.rChkSubGroupWise_CheckedChanged);
            // 
            // rChkProductWise
            // 
            this.rChkProductWise.AutoSize = true;
            this.rChkProductWise.Checked = true;
            this.rChkProductWise.Location = new System.Drawing.Point(8, 28);
            this.rChkProductWise.Name = "rChkProductWise";
            this.rChkProductWise.Size = new System.Drawing.Size(88, 24);
            this.rChkProductWise.TabIndex = 0;
            this.rChkProductWise.TabStop = true;
            this.rChkProductWise.Text = "Product";
            this.rChkProductWise.UseVisualStyleBackColor = true;
            this.rChkProductWise.CheckedChanged += new System.EventHandler(this.rChkProductWise_CheckedChanged);
            // 
            // rChkGroupWise
            // 
            this.rChkGroupWise.AutoSize = true;
            this.rChkGroupWise.Location = new System.Drawing.Point(8, 52);
            this.rChkGroupWise.Name = "rChkGroupWise";
            this.rChkGroupWise.Size = new System.Drawing.Size(143, 24);
            this.rChkGroupWise.TabIndex = 1;
            this.rChkGroupWise.Text = "Product Group";
            this.rChkGroupWise.UseVisualStyleBackColor = true;
            this.rChkGroupWise.CheckedChanged += new System.EventHandler(this.rChkGroupWise_CheckedChanged);
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.MskToDate);
            this.mrGroup1.Controls.Add(this.CmbDateType);
            this.mrGroup1.Controls.Add(this.MskFrom);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "Date Filter";
            this.mrGroup1.Location = new System.Drawing.Point(4, 4);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 10;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(558, 59);
            this.mrGroup1.TabIndex = 0;
            // 
            // MskToDate
            // 
            this.MskToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskToDate.Location = new System.Drawing.Point(418, 27);
            this.MskToDate.Mask = "00/00/0000";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MskToDate.Size = new System.Drawing.Size(134, 26);
            this.MskToDate.TabIndex = 2;
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
            this.CmbDateType.Location = new System.Drawing.Point(5, 27);
            this.CmbDateType.Name = "CmbDateType";
            this.CmbDateType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CmbDateType.Size = new System.Drawing.Size(271, 28);
            this.CmbDateType.TabIndex = 0;
            this.CmbDateType.SelectedIndexChanged += new System.EventHandler(this.CmbDateType_SelectedIndexChanged);
            this.CmbDateType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbDateType_KeyDown);
            this.CmbDateType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbDateType_KeyPress);
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskFrom.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskFrom.Location = new System.Drawing.Point(281, 27);
            this.MskFrom.Mask = "00/00/0000";
            this.MskFrom.Name = "MskFrom";
            this.MskFrom.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MskFrom.Size = new System.Drawing.Size(134, 26);
            this.MskFrom.TabIndex = 1;
            // 
            // ChkRePostValue
            // 
            this.ChkRePostValue.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkRePostValue.Location = new System.Drawing.Point(347, 91);
            this.ChkRePostValue.Name = "ChkRePostValue";
            this.ChkRePostValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkRePostValue.Size = new System.Drawing.Size(163, 26);
            this.ChkRePostValue.TabIndex = 78;
            this.ChkRePostValue.Text = "Repost Value";
            this.ChkRePostValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkRePostValue.UseVisualStyleBackColor = true;
            // 
            // FrmStockLedger
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(571, 346);
            this.Controls.Add(this.PanelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmStockLedger";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "STOCK LEDGER REPORT";
            this.Load += new System.EventHandler(this.FrmStockLedger_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmStockLedger_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            this.mrGroup5.ResumeLayout(false);
            this.mrGroup4.ResumeLayout(false);
            this.mrGroup4.PerformLayout();
            this.mrGroup3.ResumeLayout(false);
            this.mrGroup3.PerformLayout();
            this.mrGroup2.ResumeLayout(false);
            this.mrGroup2.PerformLayout();
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox ChkDate;
        private System.Windows.Forms.CheckBox ChkSelectAll;
        private System.Windows.Forms.CheckBox ChkAltQuantity;
        private System.Windows.Forms.CheckBox ChkSummary;
        private System.Windows.Forms.MaskedTextBox MskToDate;
        private System.Windows.Forms.MaskedTextBox MskFrom;
        private System.Windows.Forms.CheckBox ChkWithValue;
        private System.Windows.Forms.CheckBox ChkBatchWise;
        private System.Windows.Forms.ComboBox CmbDateType;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private System.Windows.Forms.CheckBox ChkDynamicReports;
        private System.Windows.Forms.CheckBox ChkZeroBalance;
        private System.Windows.Forms.Panel PanelHeader;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.RadioButton rChkProductWise;
        private System.Windows.Forms.RadioButton rChkSubGroupWise;
        private System.Windows.Forms.RadioButton rChkGroupWise;
        private System.Windows.Forms.RadioButton rChkNegativeOnly;
        private System.Windows.Forms.RadioButton rChkNegativeExclude;
        private System.Windows.Forms.RadioButton rChkNegativeInclude;
        private System.Windows.Forms.RadioButton RbtnQuantity;
        private System.Windows.Forms.RadioButton RbtnBarcode;
        private System.Windows.Forms.RadioButton RbtnDescrption;
        private System.Windows.Forms.CheckBox ChkOnly;
		private System.Windows.Forms.CheckBox ChkFIFO;
        private System.Windows.Forms.CheckBox ChkIncludeVat;
        private MrGroup mrGroup1;
        private MrGroup mrGroup2;
        private MrGroup mrGroup3;
        private MrGroup mrGroup4;
        private MrGroup mrGroup5;
        private System.Windows.Forms.CheckBox ChkRePostValue;
    }
}