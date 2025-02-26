using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Inventory_Report.Analysis
{
    partial class FrmProfitability
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProfitability));
            this.CmbDateType = new System.Windows.Forms.ComboBox();
            this.ChkRepostValue = new System.Windows.Forms.CheckBox();
            this.ChkIncludeFreeQty = new System.Windows.Forms.CheckBox();
            this.ChkIncludeAdditionalTerm = new System.Windows.Forms.CheckBox();
            this.ChkSelectAll = new System.Windows.Forms.CheckBox();
            this.ChkIncludeProduct = new System.Windows.Forms.CheckBox();
            this.ChkIncludeReturn = new System.Windows.Forms.CheckBox();
            this.MskToDate = new MrMaskedTextBox();
            this.MskFrom = new MrMaskedTextBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Show = new DevExpress.XtraEditors.SimpleButton();
            this.mrPanel1 = new MrPanel();
            this.mrGroup3 = new MrGroup();
            this.mrGroup2 = new MrGroup();
            this.mrGroup5 = new MrGroup();
            this.RbtnMargin = new System.Windows.Forms.RadioButton();
            this.RbtnQuantity = new System.Windows.Forms.RadioButton();
            this.RbtnDescrption = new System.Windows.Forms.RadioButton();
            this.RbtnBarcode = new System.Windows.Forms.RadioButton();
            this.rChkCustomer = new System.Windows.Forms.RadioButton();
            this.rChkProductSubGroup = new System.Windows.Forms.RadioButton();
            this.rChkProductGroup = new System.Windows.Forms.RadioButton();
            this.rChkProduct = new System.Windows.Forms.RadioButton();
            this.mrGroup1 = new MrGroup();
            this.mrGroup4 = new MrGroup();
            this.rChkBill = new System.Windows.Forms.RadioButton();
            this.mrPanel1.SuspendLayout();
            this.mrGroup3.SuspendLayout();
            this.mrGroup2.SuspendLayout();
            this.mrGroup5.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            this.mrGroup4.SuspendLayout();
            this.SuspendLayout();
            // 
            // CmbDateType
            // 
            this.CmbDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDateType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
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
            this.CmbDateType.Location = new System.Drawing.Point(8, 16);
            this.CmbDateType.Name = "CmbDateType";
            this.CmbDateType.Size = new System.Drawing.Size(224, 28);
            this.CmbDateType.TabIndex = 0;
            this.CmbDateType.SelectedIndexChanged += new System.EventHandler(this.CmbSysDateType_SelectedIndexChanged);
            // 
            // ChkRepostValue
            // 
            this.ChkRepostValue.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.ChkRepostValue.Location = new System.Drawing.Point(238, 53);
            this.ChkRepostValue.Name = "ChkRepostValue";
            this.ChkRepostValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkRepostValue.Size = new System.Drawing.Size(210, 24);
            this.ChkRepostValue.TabIndex = 4;
            this.ChkRepostValue.Text = "Re Post Value";
            this.ChkRepostValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkRepostValue.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeFreeQty
            // 
            this.ChkIncludeFreeQty.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.ChkIncludeFreeQty.Location = new System.Drawing.Point(238, 29);
            this.ChkIncludeFreeQty.Name = "ChkIncludeFreeQty";
            this.ChkIncludeFreeQty.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeFreeQty.Size = new System.Drawing.Size(210, 24);
            this.ChkIncludeFreeQty.TabIndex = 3;
            this.ChkIncludeFreeQty.Text = "Include Free";
            this.ChkIncludeFreeQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeFreeQty.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeAdditionalTerm
            // 
            this.ChkIncludeAdditionalTerm.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.ChkIncludeAdditionalTerm.Location = new System.Drawing.Point(19, 53);
            this.ChkIncludeAdditionalTerm.Name = "ChkIncludeAdditionalTerm";
            this.ChkIncludeAdditionalTerm.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeAdditionalTerm.Size = new System.Drawing.Size(210, 24);
            this.ChkIncludeAdditionalTerm.TabIndex = 1;
            this.ChkIncludeAdditionalTerm.Text = "Additional Term";
            this.ChkIncludeAdditionalTerm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeAdditionalTerm.UseVisualStyleBackColor = true;
            // 
            // ChkSelectAll
            // 
            this.ChkSelectAll.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.ChkSelectAll.Location = new System.Drawing.Point(238, 77);
            this.ChkSelectAll.Name = "ChkSelectAll";
            this.ChkSelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSelectAll.Size = new System.Drawing.Size(210, 24);
            this.ChkSelectAll.TabIndex = 5;
            this.ChkSelectAll.Text = "Select All";
            this.ChkSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSelectAll.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeProduct
            // 
            this.ChkIncludeProduct.Enabled = false;
            this.ChkIncludeProduct.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.ChkIncludeProduct.Location = new System.Drawing.Point(19, 77);
            this.ChkIncludeProduct.Name = "ChkIncludeProduct";
            this.ChkIncludeProduct.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeProduct.Size = new System.Drawing.Size(210, 24);
            this.ChkIncludeProduct.TabIndex = 2;
            this.ChkIncludeProduct.Text = "Include Product";
            this.ChkIncludeProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeProduct.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeReturn
            // 
            this.ChkIncludeReturn.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.ChkIncludeReturn.Location = new System.Drawing.Point(19, 29);
            this.ChkIncludeReturn.Name = "ChkIncludeReturn";
            this.ChkIncludeReturn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeReturn.Size = new System.Drawing.Size(210, 24);
            this.ChkIncludeReturn.TabIndex = 0;
            this.ChkIncludeReturn.Text = "Include Return Value";
            this.ChkIncludeReturn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeReturn.UseVisualStyleBackColor = true;
            // 
            // MskToDate
            // 
            this.MskToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.MskToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskToDate.Location = new System.Drawing.Point(370, 17);
            this.MskToDate.Mask = "00/00/0000";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.Size = new System.Drawing.Size(117, 26);
            this.MskToDate.TabIndex = 2;
            this.MskToDate.Enter += new System.EventHandler(this.MskToDate_Enter);
            this.MskToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MskToDate_KeyDown);
            this.MskToDate.Leave += new System.EventHandler(this.MskToDate_Leave);
            this.MskToDate.Validated += new System.EventHandler(this.MskToDate_Validated);
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.MskFrom.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskFrom.Location = new System.Drawing.Point(238, 17);
            this.MskFrom.Mask = "00/00/0000";
            this.MskFrom.Name = "MskFrom";
            this.MskFrom.Size = new System.Drawing.Size(126, 26);
            this.MskFrom.TabIndex = 1;
            this.MskFrom.Enter += new System.EventHandler(this.MskFromDate_Enter);
            this.MskFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MskFromDate_KeyDown);
            this.MskFrom.Leave += new System.EventHandler(this.MskFromDate_Leave);
            this.MskFrom.Validated += new System.EventHandler(this.MskFromDate_Validated);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(336, 15);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(106, 35);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // Btn_Show
            // 
            this.Btn_Show.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Show.Appearance.Options.UseFont = true;
            this.Btn_Show.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.Btn_Show.Location = new System.Drawing.Point(228, 15);
            this.Btn_Show.Name = "Btn_Show";
            this.Btn_Show.Size = new System.Drawing.Size(104, 35);
            this.Btn_Show.TabIndex = 0;
            this.Btn_Show.Text = "&SHOW";
            this.Btn_Show.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.mrGroup3);
            this.mrPanel1.Controls.Add(this.mrGroup2);
            this.mrPanel1.Controls.Add(this.mrGroup1);
            this.mrPanel1.Controls.Add(this.mrGroup4);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(0, 0);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(504, 353);
            this.mrPanel1.TabIndex = 0;
            // 
            // mrGroup3
            // 
            this.mrGroup3.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup3.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup3.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup3.BorderColor = System.Drawing.Color.White;
            this.mrGroup3.BorderThickness = 1F;
            this.mrGroup3.Controls.Add(this.ChkIncludeReturn);
            this.mrGroup3.Controls.Add(this.ChkRepostValue);
            this.mrGroup3.Controls.Add(this.ChkIncludeProduct);
            this.mrGroup3.Controls.Add(this.ChkIncludeFreeQty);
            this.mrGroup3.Controls.Add(this.ChkSelectAll);
            this.mrGroup3.Controls.Add(this.ChkIncludeAdditionalTerm);
            this.mrGroup3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup3.GroupImage = null;
            this.mrGroup3.GroupTitle = "Filter";
            this.mrGroup3.Location = new System.Drawing.Point(4, 193);
            this.mrGroup3.Name = "mrGroup3";
            this.mrGroup3.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup3.PaintGroupBox = false;
            this.mrGroup3.RoundCorners = 10;
            this.mrGroup3.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup3.ShadowControl = false;
            this.mrGroup3.ShadowThickness = 3;
            this.mrGroup3.Size = new System.Drawing.Size(495, 111);
            this.mrGroup3.TabIndex = 2;
            // 
            // mrGroup2
            // 
            this.mrGroup2.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup2.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup2.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup2.BorderColor = System.Drawing.Color.White;
            this.mrGroup2.BorderThickness = 1F;
            this.mrGroup2.Controls.Add(this.rChkBill);
            this.mrGroup2.Controls.Add(this.mrGroup5);
            this.mrGroup2.Controls.Add(this.rChkCustomer);
            this.mrGroup2.Controls.Add(this.rChkProductSubGroup);
            this.mrGroup2.Controls.Add(this.rChkProductGroup);
            this.mrGroup2.Controls.Add(this.rChkProduct);
            this.mrGroup2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup2.GroupImage = null;
            this.mrGroup2.GroupTitle = "Report Type";
            this.mrGroup2.Location = new System.Drawing.Point(3, 45);
            this.mrGroup2.Name = "mrGroup2";
            this.mrGroup2.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup2.PaintGroupBox = false;
            this.mrGroup2.RoundCorners = 10;
            this.mrGroup2.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup2.ShadowControl = false;
            this.mrGroup2.ShadowThickness = 3;
            this.mrGroup2.Size = new System.Drawing.Size(496, 145);
            this.mrGroup2.TabIndex = 1;
            // 
            // mrGroup5
            // 
            this.mrGroup5.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup5.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup5.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup5.BorderColor = System.Drawing.Color.White;
            this.mrGroup5.BorderThickness = 1F;
            this.mrGroup5.Controls.Add(this.RbtnMargin);
            this.mrGroup5.Controls.Add(this.RbtnQuantity);
            this.mrGroup5.Controls.Add(this.RbtnDescrption);
            this.mrGroup5.Controls.Add(this.RbtnBarcode);
            this.mrGroup5.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup5.GroupImage = null;
            this.mrGroup5.GroupTitle = "Sort On";
            this.mrGroup5.Location = new System.Drawing.Point(3, 84);
            this.mrGroup5.Name = "mrGroup5";
            this.mrGroup5.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup5.PaintGroupBox = false;
            this.mrGroup5.RoundCorners = 10;
            this.mrGroup5.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup5.ShadowControl = false;
            this.mrGroup5.ShadowThickness = 3;
            this.mrGroup5.Size = new System.Drawing.Size(486, 57);
            this.mrGroup5.TabIndex = 1;
            // 
            // RbtnMargin
            // 
            this.RbtnMargin.AutoSize = true;
            this.RbtnMargin.Location = new System.Drawing.Point(294, 27);
            this.RbtnMargin.Name = "RbtnMargin";
            this.RbtnMargin.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RbtnMargin.Size = new System.Drawing.Size(79, 23);
            this.RbtnMargin.TabIndex = 3;
            this.RbtnMargin.Text = "Margin";
            this.RbtnMargin.UseVisualStyleBackColor = true;
            // 
            // RbtnQuantity
            // 
            this.RbtnQuantity.AutoSize = true;
            this.RbtnQuantity.Location = new System.Drawing.Point(195, 27);
            this.RbtnQuantity.Name = "RbtnQuantity";
            this.RbtnQuantity.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RbtnQuantity.Size = new System.Drawing.Size(93, 23);
            this.RbtnQuantity.TabIndex = 2;
            this.RbtnQuantity.Text = "Quantity";
            this.RbtnQuantity.UseVisualStyleBackColor = true;
            // 
            // RbtnDescrption
            // 
            this.RbtnDescrption.AutoSize = true;
            this.RbtnDescrption.Checked = true;
            this.RbtnDescrption.Location = new System.Drawing.Point(23, 27);
            this.RbtnDescrption.Name = "RbtnDescrption";
            this.RbtnDescrption.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RbtnDescrption.Size = new System.Drawing.Size(70, 23);
            this.RbtnDescrption.TabIndex = 0;
            this.RbtnDescrption.TabStop = true;
            this.RbtnDescrption.Text = "Name";
            this.RbtnDescrption.UseVisualStyleBackColor = true;
            // 
            // RbtnBarcode
            // 
            this.RbtnBarcode.AutoSize = true;
            this.RbtnBarcode.Location = new System.Drawing.Point(99, 27);
            this.RbtnBarcode.Name = "RbtnBarcode";
            this.RbtnBarcode.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RbtnBarcode.Size = new System.Drawing.Size(91, 23);
            this.RbtnBarcode.TabIndex = 1;
            this.RbtnBarcode.Text = "BarCode";
            this.RbtnBarcode.UseVisualStyleBackColor = true;
            // 
            // rChkCustomer
            // 
            this.rChkCustomer.AutoSize = true;
            this.rChkCustomer.Location = new System.Drawing.Point(197, 57);
            this.rChkCustomer.Name = "rChkCustomer";
            this.rChkCustomer.Size = new System.Drawing.Size(141, 23);
            this.rChkCustomer.TabIndex = 3;
            this.rChkCustomer.Text = "Customer Wise";
            this.rChkCustomer.UseVisualStyleBackColor = true;
            // 
            // rChkProductSubGroup
            // 
            this.rChkProductSubGroup.AutoSize = true;
            this.rChkProductSubGroup.Location = new System.Drawing.Point(197, 29);
            this.rChkProductSubGroup.Name = "rChkProductSubGroup";
            this.rChkProductSubGroup.Size = new System.Drawing.Size(203, 23);
            this.rChkProductSubGroup.TabIndex = 2;
            this.rChkProductSubGroup.Text = "Product SubGroup Wise";
            this.rChkProductSubGroup.UseVisualStyleBackColor = true;
            // 
            // rChkProductGroup
            // 
            this.rChkProductGroup.AutoSize = true;
            this.rChkProductGroup.Location = new System.Drawing.Point(9, 57);
            this.rChkProductGroup.Name = "rChkProductGroup";
            this.rChkProductGroup.Size = new System.Drawing.Size(175, 23);
            this.rChkProductGroup.TabIndex = 1;
            this.rChkProductGroup.Text = "Product Group Wise";
            this.rChkProductGroup.UseVisualStyleBackColor = true;
            // 
            // rChkProduct
            // 
            this.rChkProduct.AutoSize = true;
            this.rChkProduct.Checked = true;
            this.rChkProduct.Location = new System.Drawing.Point(9, 29);
            this.rChkProduct.Name = "rChkProduct";
            this.rChkProduct.Size = new System.Drawing.Size(125, 23);
            this.rChkProduct.TabIndex = 0;
            this.rChkProduct.TabStop = true;
            this.rChkProduct.Text = "Product Wise";
            this.rChkProduct.UseVisualStyleBackColor = true;
            this.rChkProduct.CheckedChanged += new System.EventHandler(this.rChkProduct_CheckedChanged);
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
            this.mrGroup1.RoundCorners = 3;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(496, 49);
            this.mrGroup1.TabIndex = 0;
            // 
            // mrGroup4
            // 
            this.mrGroup4.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup4.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup4.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup4.BorderColor = System.Drawing.Color.White;
            this.mrGroup4.BorderThickness = 1F;
            this.mrGroup4.Controls.Add(this.BtnCancel);
            this.mrGroup4.Controls.Add(this.Btn_Show);
            this.mrGroup4.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup4.GroupImage = null;
            this.mrGroup4.GroupTitle = "";
            this.mrGroup4.Location = new System.Drawing.Point(3, 295);
            this.mrGroup4.Name = "mrGroup4";
            this.mrGroup4.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup4.PaintGroupBox = false;
            this.mrGroup4.RoundCorners = 2;
            this.mrGroup4.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup4.ShadowControl = false;
            this.mrGroup4.ShadowThickness = 3;
            this.mrGroup4.Size = new System.Drawing.Size(496, 54);
            this.mrGroup4.TabIndex = 3;
            // 
            // rChkBill
            // 
            this.rChkBill.AutoSize = true;
            this.rChkBill.Location = new System.Drawing.Point(344, 57);
            this.rChkBill.Name = "rChkBill";
            this.rChkBill.Size = new System.Drawing.Size(94, 23);
            this.rChkBill.TabIndex = 4;
            this.rChkBill.Text = "Bill Wise";
            this.rChkBill.UseVisualStyleBackColor = true;
            // 
            // FrmProfitability
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(504, 353);
            this.Controls.Add(this.mrPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmProfitability";
            this.Text = "Profitability";
            this.Load += new System.EventHandler(this.FrmProfitability_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmProfitability_KeyPress);
            this.mrPanel1.ResumeLayout(false);
            this.mrGroup3.ResumeLayout(false);
            this.mrGroup2.ResumeLayout(false);
            this.mrGroup2.PerformLayout();
            this.mrGroup5.ResumeLayout(false);
            this.mrGroup5.PerformLayout();
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.mrGroup4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox ChkIncludeFreeQty;
        private System.Windows.Forms.CheckBox ChkIncludeAdditionalTerm;
        private System.Windows.Forms.CheckBox ChkSelectAll;
        private System.Windows.Forms.CheckBox ChkIncludeProduct;
        private System.Windows.Forms.CheckBox ChkIncludeReturn;
        private System.Windows.Forms.CheckBox ChkRepostValue;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton Btn_Show;
        private System.Windows.Forms.ComboBox CmbDateType;
        private MrMaskedTextBox MskToDate;
        private MrMaskedTextBox MskFrom;
        private MrPanel mrPanel1;
        private MrGroup mrGroup1;
        private MrGroup mrGroup2;
        private System.Windows.Forms.RadioButton rChkCustomer;
        private System.Windows.Forms.RadioButton rChkProductSubGroup;
        private System.Windows.Forms.RadioButton rChkProductGroup;
        private System.Windows.Forms.RadioButton rChkProduct;
        private MrGroup mrGroup4;
        private MrGroup mrGroup3;
        private MrGroup mrGroup5;
        private System.Windows.Forms.RadioButton RbtnQuantity;
        private System.Windows.Forms.RadioButton RbtnDescrption;
        private System.Windows.Forms.RadioButton RbtnBarcode;
        private System.Windows.Forms.RadioButton RbtnMargin;
        private System.Windows.Forms.RadioButton rChkBill;
    }
}