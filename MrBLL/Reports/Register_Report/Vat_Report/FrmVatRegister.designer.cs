using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.Vat_Report
{
    partial class FrmVatRegister
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVatRegister));
            this.label4 = new System.Windows.Forms.Label();
            this.CmbDateType = new System.Windows.Forms.ComboBox();
            this.BtnSalesTerm = new System.Windows.Forms.Button();
            this.TxtSalesVatTerm = new MrTextBox();
            this.lbl_VatTerm = new System.Windows.Forms.Label();
            this.ChkDate = new System.Windows.Forms.CheckBox();
            this.MskToDate = new MrMaskedTextBox();
            this.MskFrom = new MrMaskedTextBox();
            this.btn_Show = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.PanelHeader = new MrPanel();
            this.mrGroup3 = new MrGroup();
            this.rBtnMonthly = new System.Windows.Forms.RadioButton();
            this.rBtnNormal = new System.Windows.Forms.RadioButton();
            this.rBtnLedger = new System.Windows.Forms.RadioButton();
            this.rBtnProductWise = new System.Windows.Forms.RadioButton();
            this.rbtnVoucherWise = new System.Windows.Forms.RadioButton();
            this.mrGroup2 = new MrGroup();
            this.TxtTransactionAboveAbove = new MrTextBox();
            this.ChkTransactionAbove = new System.Windows.Forms.CheckBox();
            this.ChkIncludeReturn = new System.Windows.Forms.CheckBox();
            this.ChkNepaliDesc = new System.Windows.Forms.CheckBox();
            this.ChkClaimed = new System.Windows.Forms.CheckBox();
            this.BtnPurchaseTerm = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtPurchaseVatTerm = new MrTextBox();
            this.clsSeparator1 = new ClsSeparator();
            this.mrGroup1 = new MrGroup();
            this.PanelHeader.SuspendLayout();
            this.mrGroup3.SuspendLayout();
            this.mrGroup2.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 20);
            this.label4.TabIndex = 85;
            this.label4.Text = "Date Type";
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
            this.CmbDateType.Location = new System.Drawing.Point(98, 29);
            this.CmbDateType.Name = "CmbDateType";
            this.CmbDateType.Size = new System.Drawing.Size(243, 28);
            this.CmbDateType.TabIndex = 0;
            this.CmbDateType.SelectedIndexChanged += new System.EventHandler(this.CmbSysDateType_SelectedIndexChanged);
            // 
            // BtnSalesTerm
            // 
            this.BtnSalesTerm.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSalesTerm.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnSalesTerm.Location = new System.Drawing.Point(389, 115);
            this.BtnSalesTerm.Name = "BtnSalesTerm";
            this.BtnSalesTerm.Size = new System.Drawing.Size(29, 26);
            this.BtnSalesTerm.TabIndex = 13;
            this.BtnSalesTerm.TabStop = false;
            this.BtnSalesTerm.UseVisualStyleBackColor = true;
            this.BtnSalesTerm.Click += new System.EventHandler(this.BtnSalesTerm_Click);
            // 
            // TxtSalesVatTerm
            // 
            this.TxtSalesVatTerm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSalesVatTerm.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSalesVatTerm.Location = new System.Drawing.Point(186, 115);
            this.TxtSalesVatTerm.Name = "TxtSalesVatTerm";
            this.TxtSalesVatTerm.Size = new System.Drawing.Size(197, 26);
            this.TxtSalesVatTerm.TabIndex = 5;
            this.TxtSalesVatTerm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSalesVatTerm_KeyDown);
            // 
            // lbl_VatTerm
            // 
            this.lbl_VatTerm.AutoSize = true;
            this.lbl_VatTerm.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_VatTerm.Location = new System.Drawing.Point(14, 118);
            this.lbl_VatTerm.Name = "lbl_VatTerm";
            this.lbl_VatTerm.Size = new System.Drawing.Size(132, 20);
            this.lbl_VatTerm.TabIndex = 71;
            this.lbl_VatTerm.Text = "Sales VAT Term";
            // 
            // IsDate
            // 
            this.ChkDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDate.Location = new System.Drawing.Point(15, 28);
            this.ChkDate.Name = "ChkDate";
            this.ChkDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDate.Size = new System.Drawing.Size(144, 23);
            this.ChkDate.TabIndex = 8;
            this.ChkDate.Text = "Date";
            this.ChkDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDate.UseVisualStyleBackColor = true;
            // 
            // MskToDate
            // 
            this.MskToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskToDate.Location = new System.Drawing.Point(463, 29);
            this.MskToDate.Mask = "00/00/0000";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.Size = new System.Drawing.Size(118, 26);
            this.MskToDate.TabIndex = 2;
            this.MskToDate.Validated += new System.EventHandler(this.MskToDate_Validated);
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskFrom.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskFrom.Location = new System.Drawing.Point(343, 29);
            this.MskFrom.Mask = "00/00/0000";
            this.MskFrom.Name = "MskFrom";
            this.MskFrom.Size = new System.Drawing.Size(120, 26);
            this.MskFrom.TabIndex = 1;
            this.MskFrom.Validated += new System.EventHandler(this.MskFromDate_Validated);
            // 
            // btn_Show
            // 
            this.btn_Show.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Show.Appearance.Options.UseFont = true;
            this.btn_Show.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.btn_Show.Location = new System.Drawing.Point(178, 175);
            this.btn_Show.Name = "btn_Show";
            this.btn_Show.Size = new System.Drawing.Size(97, 36);
            this.btn_Show.TabIndex = 11;
            this.btn_Show.Text = "&SHOW";
            this.btn_Show.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Appearance.Options.UseFont = true;
            this.btn_Cancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(277, 175);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(106, 36);
            this.btn_Cancel.TabIndex = 12;
            this.btn_Cancel.Text = "&CANCEL";
            this.btn_Cancel.Click += new System.EventHandler(this.BtnCancel_Click);
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
            this.PanelHeader.Size = new System.Drawing.Size(591, 288);
            this.PanelHeader.TabIndex = 0;
            // 
            // mrGroup3
            // 
            this.mrGroup3.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup3.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup3.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup3.BorderColor = System.Drawing.Color.White;
            this.mrGroup3.BorderThickness = 1F;
            this.mrGroup3.Controls.Add(this.rBtnMonthly);
            this.mrGroup3.Controls.Add(this.rBtnNormal);
            this.mrGroup3.Controls.Add(this.rBtnLedger);
            this.mrGroup3.Controls.Add(this.rBtnProductWise);
            this.mrGroup3.Controls.Add(this.rbtnVoucherWise);
            this.mrGroup3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup3.GroupImage = null;
            this.mrGroup3.GroupTitle = "Report Type";
            this.mrGroup3.Location = new System.Drawing.Point(3, 68);
            this.mrGroup3.Name = "mrGroup3";
            this.mrGroup3.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup3.PaintGroupBox = false;
            this.mrGroup3.RoundCorners = 10;
            this.mrGroup3.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup3.ShadowControl = true;
            this.mrGroup3.ShadowThickness = 3;
            this.mrGroup3.Size = new System.Drawing.Size(157, 219);
            this.mrGroup3.TabIndex = 92;
            // 
            // rBtnMonthly
            // 
            this.rBtnMonthly.AutoSize = true;
            this.rBtnMonthly.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.rBtnMonthly.Location = new System.Drawing.Point(8, 129);
            this.rBtnMonthly.Name = "rBtnMonthly";
            this.rBtnMonthly.Size = new System.Drawing.Size(93, 24);
            this.rBtnMonthly.TabIndex = 4;
            this.rBtnMonthly.Text = "Monthly";
            this.rBtnMonthly.UseVisualStyleBackColor = true;
            // 
            // rBtnNormal
            // 
            this.rBtnNormal.AutoSize = true;
            this.rBtnNormal.Checked = true;
            this.rBtnNormal.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.rBtnNormal.Location = new System.Drawing.Point(8, 33);
            this.rBtnNormal.Name = "rBtnNormal";
            this.rBtnNormal.Size = new System.Drawing.Size(84, 24);
            this.rBtnNormal.TabIndex = 0;
            this.rBtnNormal.TabStop = true;
            this.rBtnNormal.Text = "Normal";
            this.rBtnNormal.UseVisualStyleBackColor = true;
            this.rBtnNormal.CheckedChanged += new System.EventHandler(this.RBtnNormal_CheckedChanged);
            // 
            // rBtnLedger
            // 
            this.rBtnLedger.AutoSize = true;
            this.rBtnLedger.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.rBtnLedger.Location = new System.Drawing.Point(8, 105);
            this.rBtnLedger.Name = "rBtnLedger";
            this.rBtnLedger.Size = new System.Drawing.Size(81, 24);
            this.rBtnLedger.TabIndex = 3;
            this.rBtnLedger.Text = "Ledger";
            this.rBtnLedger.UseVisualStyleBackColor = true;
            // 
            // rBtnProductWise
            // 
            this.rBtnProductWise.AutoSize = true;
            this.rBtnProductWise.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.rBtnProductWise.Location = new System.Drawing.Point(8, 57);
            this.rBtnProductWise.Name = "rBtnProductWise";
            this.rBtnProductWise.Size = new System.Drawing.Size(88, 24);
            this.rBtnProductWise.TabIndex = 1;
            this.rBtnProductWise.Text = "Product";
            this.rBtnProductWise.UseVisualStyleBackColor = true;
            // 
            // rbtnVoucherWise
            // 
            this.rbtnVoucherWise.AutoSize = true;
            this.rbtnVoucherWise.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.rbtnVoucherWise.Location = new System.Drawing.Point(8, 81);
            this.rbtnVoucherWise.Name = "rbtnVoucherWise";
            this.rbtnVoucherWise.Size = new System.Drawing.Size(93, 24);
            this.rbtnVoucherWise.TabIndex = 2;
            this.rbtnVoucherWise.Text = "Voucher";
            this.rbtnVoucherWise.UseVisualStyleBackColor = true;
            // 
            // mrGroup2
            // 
            this.mrGroup2.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup2.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup2.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup2.BorderColor = System.Drawing.Color.White;
            this.mrGroup2.BorderThickness = 1F;
            this.mrGroup2.Controls.Add(this.TxtTransactionAboveAbove);
            this.mrGroup2.Controls.Add(this.ChkTransactionAbove);
            this.mrGroup2.Controls.Add(this.ChkIncludeReturn);
            this.mrGroup2.Controls.Add(this.ChkNepaliDesc);
            this.mrGroup2.Controls.Add(this.ChkClaimed);
            this.mrGroup2.Controls.Add(this.ChkDate);
            this.mrGroup2.Controls.Add(this.TxtSalesVatTerm);
            this.mrGroup2.Controls.Add(this.BtnPurchaseTerm);
            this.mrGroup2.Controls.Add(this.lbl_VatTerm);
            this.mrGroup2.Controls.Add(this.label3);
            this.mrGroup2.Controls.Add(this.TxtPurchaseVatTerm);
            this.mrGroup2.Controls.Add(this.BtnSalesTerm);
            this.mrGroup2.Controls.Add(this.clsSeparator1);
            this.mrGroup2.Controls.Add(this.btn_Show);
            this.mrGroup2.Controls.Add(this.btn_Cancel);
            this.mrGroup2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup2.GroupImage = null;
            this.mrGroup2.GroupTitle = "Filter Value";
            this.mrGroup2.Location = new System.Drawing.Point(160, 68);
            this.mrGroup2.Name = "mrGroup2";
            this.mrGroup2.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup2.PaintGroupBox = false;
            this.mrGroup2.RoundCorners = 10;
            this.mrGroup2.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup2.ShadowControl = true;
            this.mrGroup2.ShadowThickness = 3;
            this.mrGroup2.Size = new System.Drawing.Size(431, 219);
            this.mrGroup2.TabIndex = 91;
            // 
            // TxtTransactionAboveAbove
            // 
            this.TxtTransactionAboveAbove.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTransactionAboveAbove.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTransactionAboveAbove.Location = new System.Drawing.Point(186, 87);
            this.TxtTransactionAboveAbove.Name = "TxtTransactionAboveAbove";
            this.TxtTransactionAboveAbove.Size = new System.Drawing.Size(197, 26);
            this.TxtTransactionAboveAbove.TabIndex = 94;
            // 
            // ChkTransactionAbove
            // 
            this.ChkTransactionAbove.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkTransactionAbove.Location = new System.Drawing.Point(165, 59);
            this.ChkTransactionAbove.Name = "ChkTransactionAbove";
            this.ChkTransactionAbove.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkTransactionAbove.Size = new System.Drawing.Size(178, 23);
            this.ChkTransactionAbove.TabIndex = 93;
            this.ChkTransactionAbove.Text = "Transaction Above";
            this.ChkTransactionAbove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkTransactionAbove.UseVisualStyleBackColor = true;
            this.ChkTransactionAbove.CheckedChanged += new System.EventHandler(this.ChkTransactionAbove_CheckedChanged);
            // 
            // ChkIncludeReturn
            // 
            this.ChkIncludeReturn.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeReturn.Location = new System.Drawing.Point(15, 59);
            this.ChkIncludeReturn.Name = "ChkIncludeReturn";
            this.ChkIncludeReturn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeReturn.Size = new System.Drawing.Size(144, 23);
            this.ChkIncludeReturn.TabIndex = 92;
            this.ChkIncludeReturn.Text = "Include Return ";
            this.ChkIncludeReturn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeReturn.UseVisualStyleBackColor = true;
            // 
            // ChkNepaliDesc
            // 
            this.ChkNepaliDesc.AutoCheck = false;
            this.ChkNepaliDesc.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkNepaliDesc.Location = new System.Drawing.Point(15, 90);
            this.ChkNepaliDesc.Name = "ChkNepaliDesc";
            this.ChkNepaliDesc.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkNepaliDesc.Size = new System.Drawing.Size(144, 23);
            this.ChkNepaliDesc.TabIndex = 91;
            this.ChkNepaliDesc.Text = "Nepali Desc";
            this.ChkNepaliDesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkNepaliDesc.UseVisualStyleBackColor = true;
            // 
            // ChkClaimed
            // 
            this.ChkClaimed.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkClaimed.Location = new System.Drawing.Point(164, 28);
            this.ChkClaimed.Name = "ChkClaimed";
            this.ChkClaimed.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkClaimed.Size = new System.Drawing.Size(179, 23);
            this.ChkClaimed.TabIndex = 90;
            this.ChkClaimed.Text = "Claimed";
            this.ChkClaimed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkClaimed.UseVisualStyleBackColor = true;
            // 
            // BtnPurchaseTerm
            // 
            this.BtnPurchaseTerm.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPurchaseTerm.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnPurchaseTerm.Location = new System.Drawing.Point(389, 142);
            this.BtnPurchaseTerm.Name = "BtnPurchaseTerm";
            this.BtnPurchaseTerm.Size = new System.Drawing.Size(29, 26);
            this.BtnPurchaseTerm.TabIndex = 88;
            this.BtnPurchaseTerm.TabStop = false;
            this.BtnPurchaseTerm.UseVisualStyleBackColor = true;
            this.BtnPurchaseTerm.Click += new System.EventHandler(this.BtnPurchaseTerm_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 20);
            this.label3.TabIndex = 89;
            this.label3.Text = "Purchase VAT Term";
            // 
            // TxtPurchaseVatTerm
            // 
            this.TxtPurchaseVatTerm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPurchaseVatTerm.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPurchaseVatTerm.Location = new System.Drawing.Point(186, 142);
            this.TxtPurchaseVatTerm.Name = "TxtPurchaseVatTerm";
            this.TxtPurchaseVatTerm.Size = new System.Drawing.Size(197, 26);
            this.TxtPurchaseVatTerm.TabIndex = 87;
            this.TxtPurchaseVatTerm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPurchaseVatTerm_KeyDown);
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator1.Location = new System.Drawing.Point(5, 171);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(416, 2);
            this.clsSeparator1.TabIndex = 86;
            this.clsSeparator1.TabStop = false;
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.CmbDateType);
            this.mrGroup1.Controls.Add(this.label4);
            this.mrGroup1.Controls.Add(this.MskFrom);
            this.mrGroup1.Controls.Add(this.MskToDate);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "Date Filter";
            this.mrGroup1.Location = new System.Drawing.Point(3, 1);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 10;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = true;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(588, 67);
            this.mrGroup1.TabIndex = 90;
            // 
            // FrmVatRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(591, 288);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmVatRegister";
            this.Text = "VAT REGISTER REPORTS";
            this.Load += new System.EventHandler(this.FrmVatRegister_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmVatRegister_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            this.mrGroup3.ResumeLayout(false);
            this.mrGroup3.PerformLayout();
            this.mrGroup2.ResumeLayout(false);
            this.mrGroup2.PerformLayout();
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button BtnSalesTerm;
        private System.Windows.Forms.Label lbl_VatTerm;
        private System.Windows.Forms.CheckBox ChkDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CmbDateType;
        private DevExpress.XtraEditors.SimpleButton btn_Show;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.Button BtnPurchaseTerm;
        private System.Windows.Forms.Label label3;
        private MrTextBox TxtSalesVatTerm;
        private MrMaskedTextBox MskToDate;
        private MrMaskedTextBox MskFrom;
        private MrPanel PanelHeader;
        private MrTextBox TxtPurchaseVatTerm;
        private MrGroup mrGroup1;
        private MrGroup mrGroup2;
        private System.Windows.Forms.CheckBox ChkClaimed;
        private System.Windows.Forms.CheckBox ChkNepaliDesc;
        private System.Windows.Forms.CheckBox ChkIncludeReturn;
        private MrGroup mrGroup3;
        private System.Windows.Forms.RadioButton rBtnMonthly;
        private System.Windows.Forms.RadioButton rBtnNormal;
        private System.Windows.Forms.RadioButton rBtnLedger;
        private System.Windows.Forms.RadioButton rBtnProductWise;
        private System.Windows.Forms.RadioButton rbtnVoucherWise;
        private System.Windows.Forms.CheckBox ChkTransactionAbove;
        private MrTextBox TxtTransactionAboveAbove;
    }
}