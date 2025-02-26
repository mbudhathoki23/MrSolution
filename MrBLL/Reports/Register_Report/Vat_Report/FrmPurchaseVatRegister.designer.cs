using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.Vat_Report
{
    partial class FrmPurchaseVatRegister
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPurchaseVatRegister));
            this.mrGroup3 = new MrGroup();
            this.ChkNepaliDesc = new System.Windows.Forms.CheckBox();
            this.TxtPurchaseAbove = new MrTextBox();
            this.TxtVatTerm = new MrTextBox();
            this.btn_Term = new System.Windows.Forms.Button();
            this.ChkIncludeReturn = new System.Windows.Forms.CheckBox();
            this.lbl_VatTerm = new System.Windows.Forms.Label();
            this.ChkSummary = new System.Windows.Forms.CheckBox();
            this.lbl_SalesAbove = new System.Windows.Forms.Label();
            this.ChkRemarks = new System.Windows.Forms.CheckBox();
            this.ChkDate = new System.Windows.Forms.CheckBox();
            this.btn_Show = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new MrPanel();
            this.mrGroup2 = new MrGroup();
            this.rBtnMonthly = new System.Windows.Forms.RadioButton();
            this.rBtnNormal = new System.Windows.Forms.RadioButton();
            this.rBtnCustomer = new System.Windows.Forms.RadioButton();
            this.rBtnProductWise = new System.Windows.Forms.RadioButton();
            this.rbtnVoucherWise = new System.Windows.Forms.RadioButton();
            this.mrGroup1 = new MrGroup();
            this.CmbDateType = new System.Windows.Forms.ComboBox();
            this.MskFrom = new MrMaskedTextBox();
            this.MskToDate = new MrMaskedTextBox();
            this.mrGroup4 = new MrGroup();
            this.mrGroup3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.mrGroup2.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            this.mrGroup4.SuspendLayout();
            this.SuspendLayout();
            // 
            // mrGroup3
            // 
            this.mrGroup3.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup3.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup3.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup3.BorderColor = System.Drawing.Color.White;
            this.mrGroup3.BorderThickness = 1F;
            this.mrGroup3.Controls.Add(this.ChkNepaliDesc);
            this.mrGroup3.Controls.Add(this.TxtPurchaseAbove);
            this.mrGroup3.Controls.Add(this.TxtVatTerm);
            this.mrGroup3.Controls.Add(this.btn_Term);
            this.mrGroup3.Controls.Add(this.ChkIncludeReturn);
            this.mrGroup3.Controls.Add(this.lbl_VatTerm);
            this.mrGroup3.Controls.Add(this.ChkSummary);
            this.mrGroup3.Controls.Add(this.lbl_SalesAbove);
            this.mrGroup3.Controls.Add(this.ChkRemarks);
            this.mrGroup3.Controls.Add(this.ChkDate);
            this.mrGroup3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup3.GroupImage = null;
            this.mrGroup3.GroupTitle = "Filter Value";
            this.mrGroup3.Location = new System.Drawing.Point(166, 61);
            this.mrGroup3.Name = "mrGroup3";
            this.mrGroup3.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup3.PaintGroupBox = false;
            this.mrGroup3.RoundCorners = 10;
            this.mrGroup3.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup3.ShadowControl = false;
            this.mrGroup3.ShadowThickness = 3;
            this.mrGroup3.Size = new System.Drawing.Size(308, 156);
            this.mrGroup3.TabIndex = 2;
            // 
            // ChkNepaliDesc
            // 
            this.ChkNepaliDesc.Checked = true;
            this.ChkNepaliDesc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkNepaliDesc.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkNepaliDesc.Location = new System.Drawing.Point(117, 50);
            this.ChkNepaliDesc.Name = "ChkNepaliDesc";
            this.ChkNepaliDesc.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkNepaliDesc.Size = new System.Drawing.Size(144, 23);
            this.ChkNepaliDesc.TabIndex = 4;
            this.ChkNepaliDesc.Text = "Nepali Desc";
            this.ChkNepaliDesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkNepaliDesc.UseVisualStyleBackColor = true;
            // 
            // TxtPurchaseAbove
            // 
            this.TxtPurchaseAbove.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPurchaseAbove.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.TxtPurchaseAbove.Location = new System.Drawing.Point(138, 127);
            this.TxtPurchaseAbove.Name = "TxtPurchaseAbove";
            this.TxtPurchaseAbove.Size = new System.Drawing.Size(165, 26);
            this.TxtPurchaseAbove.TabIndex = 6;
            this.TxtPurchaseAbove.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtPurchaseAbove.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSalesAbove_KeyDown);
            this.TxtPurchaseAbove.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSalesAbove_KeyPress);
            // 
            // TxtVatTerm
            // 
            this.TxtVatTerm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtVatTerm.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.TxtVatTerm.Location = new System.Drawing.Point(138, 100);
            this.TxtVatTerm.Name = "TxtVatTerm";
            this.TxtVatTerm.Size = new System.Drawing.Size(133, 26);
            this.TxtVatTerm.TabIndex = 5;
            this.TxtVatTerm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtVatTerm_KeyDown);
            // 
            // btn_Term
            // 
            this.btn_Term.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Term.Image = global::MrBLL.Properties.Resources.search16;
            this.btn_Term.Location = new System.Drawing.Point(273, 100);
            this.btn_Term.Name = "btn_Term";
            this.btn_Term.Size = new System.Drawing.Size(31, 26);
            this.btn_Term.TabIndex = 13;
            this.btn_Term.TabStop = false;
            this.btn_Term.UseVisualStyleBackColor = true;
            this.btn_Term.Click += new System.EventHandler(this.BtnTerm_Click);
            // 
            // ChkIncludeReturn
            // 
            this.ChkIncludeReturn.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeReturn.Location = new System.Drawing.Point(117, 28);
            this.ChkIncludeReturn.Name = "ChkIncludeReturn";
            this.ChkIncludeReturn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeReturn.Size = new System.Drawing.Size(144, 23);
            this.ChkIncludeReturn.TabIndex = 3;
            this.ChkIncludeReturn.Text = "Include Return ";
            this.ChkIncludeReturn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeReturn.UseVisualStyleBackColor = true;
            // 
            // lbl_VatTerm
            // 
            this.lbl_VatTerm.AutoSize = true;
            this.lbl_VatTerm.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_VatTerm.Location = new System.Drawing.Point(7, 103);
            this.lbl_VatTerm.Name = "lbl_VatTerm";
            this.lbl_VatTerm.Size = new System.Drawing.Size(85, 20);
            this.lbl_VatTerm.TabIndex = 71;
            this.lbl_VatTerm.Text = "VAT Term";
            // 
            // ChkSummary
            // 
            this.ChkSummary.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.ChkSummary.Location = new System.Drawing.Point(7, 28);
            this.ChkSummary.Name = "ChkSummary";
            this.ChkSummary.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSummary.Size = new System.Drawing.Size(105, 23);
            this.ChkSummary.TabIndex = 0;
            this.ChkSummary.Text = "Summary";
            this.ChkSummary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSummary.UseVisualStyleBackColor = true;
            this.ChkSummary.CheckedChanged += new System.EventHandler(this.ChkSummary_CheckedChanged);
            // 
            // lbl_SalesAbove
            // 
            this.lbl_SalesAbove.AutoSize = true;
            this.lbl_SalesAbove.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_SalesAbove.Location = new System.Drawing.Point(7, 129);
            this.lbl_SalesAbove.Name = "lbl_SalesAbove";
            this.lbl_SalesAbove.Size = new System.Drawing.Size(133, 20);
            this.lbl_SalesAbove.TabIndex = 80;
            this.lbl_SalesAbove.Text = "Purchase Above";
            // 
            // ChkRemarks
            // 
            this.ChkRemarks.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.ChkRemarks.Location = new System.Drawing.Point(7, 52);
            this.ChkRemarks.Name = "ChkRemarks";
            this.ChkRemarks.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkRemarks.Size = new System.Drawing.Size(105, 21);
            this.ChkRemarks.TabIndex = 1;
            this.ChkRemarks.Text = "Remarks";
            this.ChkRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkRemarks.UseVisualStyleBackColor = true;
            // 
            // ChkDate
            // 
            this.ChkDate.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDate.Location = new System.Drawing.Point(7, 76);
            this.ChkDate.Name = "ChkDate";
            this.ChkDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDate.Size = new System.Drawing.Size(105, 21);
            this.ChkDate.TabIndex = 2;
            this.ChkDate.Text = "Date";
            this.ChkDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDate.UseVisualStyleBackColor = true;
            // 
            // btn_Show
            // 
            this.btn_Show.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Show.Appearance.Options.UseFont = true;
            this.btn_Show.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.btn_Show.Location = new System.Drawing.Point(189, 19);
            this.btn_Show.Name = "btn_Show";
            this.btn_Show.Size = new System.Drawing.Size(97, 35);
            this.btn_Show.TabIndex = 0;
            this.btn_Show.Text = "&SHOW";
            this.btn_Show.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Appearance.Options.UseFont = true;
            this.btn_Cancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(288, 19);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(106, 35);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "&CANCEL";
            this.btn_Cancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.mrGroup3);
            this.panel1.Controls.Add(this.mrGroup2);
            this.panel1.Controls.Add(this.mrGroup1);
            this.panel1.Controls.Add(this.mrGroup4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(475, 273);
            this.panel1.TabIndex = 1;
            // 
            // mrGroup2
            // 
            this.mrGroup2.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup2.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup2.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup2.BorderColor = System.Drawing.Color.White;
            this.mrGroup2.BorderThickness = 1F;
            this.mrGroup2.Controls.Add(this.rBtnMonthly);
            this.mrGroup2.Controls.Add(this.rBtnNormal);
            this.mrGroup2.Controls.Add(this.rBtnCustomer);
            this.mrGroup2.Controls.Add(this.rBtnProductWise);
            this.mrGroup2.Controls.Add(this.rbtnVoucherWise);
            this.mrGroup2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup2.GroupImage = null;
            this.mrGroup2.GroupTitle = "Report Type";
            this.mrGroup2.Location = new System.Drawing.Point(3, 61);
            this.mrGroup2.Name = "mrGroup2";
            this.mrGroup2.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup2.PaintGroupBox = false;
            this.mrGroup2.RoundCorners = 10;
            this.mrGroup2.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup2.ShadowControl = false;
            this.mrGroup2.ShadowThickness = 3;
            this.mrGroup2.Size = new System.Drawing.Size(157, 156);
            this.mrGroup2.TabIndex = 1;
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
            // 
            // rBtnCustomer
            // 
            this.rBtnCustomer.AutoSize = true;
            this.rBtnCustomer.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.rBtnCustomer.Location = new System.Drawing.Point(8, 105);
            this.rBtnCustomer.Name = "rBtnCustomer";
            this.rBtnCustomer.Size = new System.Drawing.Size(84, 24);
            this.rBtnCustomer.TabIndex = 3;
            this.rBtnCustomer.Text = "Vendor";
            this.rBtnCustomer.UseVisualStyleBackColor = true;
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
            this.mrGroup1.Font = new System.Drawing.Font("Bookman Old Style", 12F);
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
            this.mrGroup1.Size = new System.Drawing.Size(471, 61);
            this.mrGroup1.TabIndex = 0;
            // 
            // CmbDateType
            // 
            this.CmbDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDateType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbDateType.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbDateType.FormattingEnabled = true;
            this.CmbDateType.Location = new System.Drawing.Point(5, 27);
            this.CmbDateType.Name = "CmbDateType";
            this.CmbDateType.Size = new System.Drawing.Size(209, 27);
            this.CmbDateType.TabIndex = 0;
            this.CmbDateType.SelectedIndexChanged += new System.EventHandler(this.CmbSysDateType_SelectedIndexChanged);
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskFrom.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskFrom.Location = new System.Drawing.Point(216, 28);
            this.MskFrom.Mask = "00/00/0000";
            this.MskFrom.Name = "MskFrom";
            this.MskFrom.Size = new System.Drawing.Size(120, 25);
            this.MskFrom.TabIndex = 1;
            this.MskFrom.Validated += new System.EventHandler(this.MskFromDate_Validated);
            // 
            // MskToDate
            // 
            this.MskToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskToDate.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskToDate.Location = new System.Drawing.Point(338, 28);
            this.MskToDate.Mask = "00/00/0000";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.Size = new System.Drawing.Size(120, 25);
            this.MskToDate.TabIndex = 2;
            this.MskToDate.Validated += new System.EventHandler(this.MskToDate_Validated);
            // 
            // mrGroup4
            // 
            this.mrGroup4.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup4.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup4.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup4.BorderColor = System.Drawing.Color.White;
            this.mrGroup4.BorderThickness = 1F;
            this.mrGroup4.Controls.Add(this.btn_Show);
            this.mrGroup4.Controls.Add(this.btn_Cancel);
            this.mrGroup4.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup4.GroupImage = null;
            this.mrGroup4.GroupTitle = "";
            this.mrGroup4.Location = new System.Drawing.Point(2, 210);
            this.mrGroup4.Name = "mrGroup4";
            this.mrGroup4.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup4.PaintGroupBox = false;
            this.mrGroup4.RoundCorners = 10;
            this.mrGroup4.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup4.ShadowControl = true;
            this.mrGroup4.ShadowThickness = 3;
            this.mrGroup4.Size = new System.Drawing.Size(470, 62);
            this.mrGroup4.TabIndex = 3;
            // 
            // FrmPurchaseVatRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(475, 273);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPurchaseVatRegister";
            this.ShowIcon = false;
            this.Text = "PURCHASE VAT  REGISTER";
            this.Load += new System.EventHandler(this.PurchaseVatRegister_Load);
            this.Shown += new System.EventHandler(this.FrmPurchaseVatRegister_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PurchaseVatRegister_KeyPress);
            this.mrGroup3.ResumeLayout(false);
            this.mrGroup3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.mrGroup2.ResumeLayout(false);
            this.mrGroup2.PerformLayout();
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.mrGroup4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox TxtSalesAbove;
        private MrGroup mrGroup3;
        private System.Windows.Forms.CheckBox ChkNepaliDesc;
        private MrTextBox TxtVatTerm;
        private System.Windows.Forms.Button btn_Term;
        private System.Windows.Forms.CheckBox ChkIncludeReturn;
        private System.Windows.Forms.Label lbl_VatTerm;
        private System.Windows.Forms.CheckBox ChkSummary;
        private System.Windows.Forms.Label lbl_SalesAbove;
        private System.Windows.Forms.CheckBox ChkRemarks;
        private System.Windows.Forms.CheckBox ChkDate;
        private DevExpress.XtraEditors.SimpleButton btn_Show;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private MrPanel panel1;
        private MrGroup mrGroup2;
        private System.Windows.Forms.RadioButton rBtnMonthly;
        private System.Windows.Forms.RadioButton rBtnNormal;
        private System.Windows.Forms.RadioButton rBtnCustomer;
        private System.Windows.Forms.RadioButton rBtnProductWise;
        private System.Windows.Forms.RadioButton rbtnVoucherWise;
        private MrGroup mrGroup1;
        private System.Windows.Forms.ComboBox CmbDateType;
        private MrMaskedTextBox MskFrom;
        private MrMaskedTextBox MskToDate;
        private MrTextBox TxtPurchaseAbove;
        private MrGroup mrGroup4;
    }
}