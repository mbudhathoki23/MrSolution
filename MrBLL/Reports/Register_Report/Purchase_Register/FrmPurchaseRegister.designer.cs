using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.Purchase_Register
{
    partial class FrmPurchaseRegister
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPurchaseRegister));
            this.panel1 = new MrPanel();
            this.mrGroup4 = new MrGroup();
            this.rChkCancelRegister = new System.Windows.Forms.RadioButton();
            this.rChkReturnRegister = new System.Windows.Forms.RadioButton();
            this.rChkNormalRegister = new System.Windows.Forms.RadioButton();
            this.TxtVoucherNo = new MrTextBox();
            this.lbl_Find = new System.Windows.Forms.Label();
            this.mrGroup3 = new MrGroup();
            this.rChkDate = new System.Windows.Forms.RadioButton();
            this.rChkInvoice = new System.Windows.Forms.RadioButton();
            this.rChkProductGroup = new System.Windows.Forms.RadioButton();
            this.rChkDepartment = new System.Windows.Forms.RadioButton();
            this.rChkProduct = new System.Windows.Forms.RadioButton();
            this.rChkVendor = new System.Windows.Forms.RadioButton();
            this.rChkProductSubGroup = new System.Windows.Forms.RadioButton();
            this.rChkAgent = new System.Windows.Forms.RadioButton();
            this.rChkArea = new System.Windows.Forms.RadioButton();
            this.mrGroup1 = new MrGroup();
            this.rChkNormal = new System.Windows.Forms.RadioButton();
            this.rChkOtherSales = new System.Windows.Forms.RadioButton();
            this.rChkCreditSales = new System.Windows.Forms.RadioButton();
            this.rChkAllType = new System.Windows.Forms.RadioButton();
            this.rChkCashSales = new System.Windows.Forms.RadioButton();
            this.rChkCardSales = new System.Windows.Forms.RadioButton();
            this.mrGroup2 = new MrGroup();
            this.MskFrom = new MrMaskedTextBox();
            this.MskToDate = new MrMaskedTextBox();
            this.CmbDateType = new System.Windows.Forms.ComboBox();
            this.mrGroup5 = new MrGroup();
            this.ChkRefVno = new System.Windows.Forms.CheckBox();
            this.ChkSummary = new System.Windows.Forms.CheckBox();
            this.clsSeparator1 = new ClsSeparator();
            this.ChkIncludeAltQty = new System.Windows.Forms.CheckBox();
            this.ChkIncludeGodown = new System.Windows.Forms.CheckBox();
            this.ChkFreeQty = new System.Windows.Forms.CheckBox();
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.ChkDateWise = new System.Windows.Forms.CheckBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.ChkIncludeRemarks = new System.Windows.Forms.CheckBox();
            this.ChkDocAgent = new System.Windows.Forms.CheckBox();
            this.ChkAdditionalTerm = new System.Windows.Forms.CheckBox();
            this.chk_ViewDxReport = new System.Windows.Forms.CheckBox();
            this.ChkHorizontal = new System.Windows.Forms.CheckBox();
            this.ChkIncludeOrderNo = new System.Windows.Forms.CheckBox();
            this.chk_SelectAll = new System.Windows.Forms.CheckBox();
            this.ChkIncludeChallanNo = new System.Windows.Forms.CheckBox();
            this.ChkIncludeBatch = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.mrGroup4.SuspendLayout();
            this.mrGroup3.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            this.mrGroup2.SuspendLayout();
            this.mrGroup5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.mrGroup4);
            this.panel1.Controls.Add(this.mrGroup3);
            this.panel1.Controls.Add(this.mrGroup1);
            this.panel1.Controls.Add(this.mrGroup2);
            this.panel1.Controls.Add(this.mrGroup5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(545, 423);
            this.panel1.TabIndex = 0;
            // 
            // mrGroup4
            // 
            this.mrGroup4.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup4.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup4.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup4.BorderColor = System.Drawing.Color.White;
            this.mrGroup4.BorderThickness = 1F;
            this.mrGroup4.Controls.Add(this.rChkCancelRegister);
            this.mrGroup4.Controls.Add(this.rChkReturnRegister);
            this.mrGroup4.Controls.Add(this.rChkNormalRegister);
            this.mrGroup4.Controls.Add(this.TxtVoucherNo);
            this.mrGroup4.Controls.Add(this.lbl_Find);
            this.mrGroup4.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup4.GroupImage = null;
            this.mrGroup4.GroupTitle = "Report Mode";
            this.mrGroup4.Location = new System.Drawing.Point(311, 91);
            this.mrGroup4.Name = "mrGroup4";
            this.mrGroup4.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup4.PaintGroupBox = false;
            this.mrGroup4.RoundCorners = 10;
            this.mrGroup4.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup4.ShadowControl = false;
            this.mrGroup4.ShadowThickness = 3;
            this.mrGroup4.Size = new System.Drawing.Size(232, 151);
            this.mrGroup4.TabIndex = 3;
            // 
            // rChkCancelRegister
            // 
            this.rChkCancelRegister.AutoSize = true;
            this.rChkCancelRegister.Location = new System.Drawing.Point(20, 75);
            this.rChkCancelRegister.Name = "rChkCancelRegister";
            this.rChkCancelRegister.Size = new System.Drawing.Size(147, 23);
            this.rChkCancelRegister.TabIndex = 2;
            this.rChkCancelRegister.Text = "Cancel Register";
            this.rChkCancelRegister.UseVisualStyleBackColor = true;
            // 
            // rChkReturnRegister
            // 
            this.rChkReturnRegister.AutoSize = true;
            this.rChkReturnRegister.Location = new System.Drawing.Point(20, 52);
            this.rChkReturnRegister.Name = "rChkReturnRegister";
            this.rChkReturnRegister.Size = new System.Drawing.Size(148, 23);
            this.rChkReturnRegister.TabIndex = 1;
            this.rChkReturnRegister.Text = "Return Register";
            this.rChkReturnRegister.UseVisualStyleBackColor = true;
            // 
            // rChkNormalRegister
            // 
            this.rChkNormalRegister.AutoSize = true;
            this.rChkNormalRegister.Checked = true;
            this.rChkNormalRegister.Location = new System.Drawing.Point(20, 29);
            this.rChkNormalRegister.Name = "rChkNormalRegister";
            this.rChkNormalRegister.Size = new System.Drawing.Size(149, 23);
            this.rChkNormalRegister.TabIndex = 0;
            this.rChkNormalRegister.TabStop = true;
            this.rChkNormalRegister.Text = "Normal Register";
            this.rChkNormalRegister.UseVisualStyleBackColor = true;
            // 
            // TxtVoucherNo
            // 
            this.TxtVoucherNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtVoucherNo.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtVoucherNo.Location = new System.Drawing.Point(23, 121);
            this.TxtVoucherNo.Name = "TxtVoucherNo";
            this.TxtVoucherNo.Size = new System.Drawing.Size(176, 25);
            this.TxtVoucherNo.TabIndex = 3;
            // 
            // lbl_Find
            // 
            this.lbl_Find.AutoSize = true;
            this.lbl_Find.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Find.Location = new System.Drawing.Point(22, 99);
            this.lbl_Find.Name = "lbl_Find";
            this.lbl_Find.Size = new System.Drawing.Size(100, 19);
            this.lbl_Find.TabIndex = 23;
            this.lbl_Find.Text = "Find Invoice";
            // 
            // mrGroup3
            // 
            this.mrGroup3.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup3.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup3.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup3.BorderColor = System.Drawing.Color.White;
            this.mrGroup3.BorderThickness = 1F;
            this.mrGroup3.Controls.Add(this.rChkDate);
            this.mrGroup3.Controls.Add(this.rChkInvoice);
            this.mrGroup3.Controls.Add(this.rChkProductGroup);
            this.mrGroup3.Controls.Add(this.rChkDepartment);
            this.mrGroup3.Controls.Add(this.rChkProduct);
            this.mrGroup3.Controls.Add(this.rChkVendor);
            this.mrGroup3.Controls.Add(this.rChkProductSubGroup);
            this.mrGroup3.Controls.Add(this.rChkAgent);
            this.mrGroup3.Controls.Add(this.rChkArea);
            this.mrGroup3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup3.GroupImage = null;
            this.mrGroup3.GroupTitle = "Report Type";
            this.mrGroup3.Location = new System.Drawing.Point(3, 91);
            this.mrGroup3.Name = "mrGroup3";
            this.mrGroup3.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup3.PaintGroupBox = false;
            this.mrGroup3.RoundCorners = 10;
            this.mrGroup3.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup3.ShadowControl = false;
            this.mrGroup3.ShadowThickness = 3;
            this.mrGroup3.Size = new System.Drawing.Size(306, 151);
            this.mrGroup3.TabIndex = 2;
            // 
            // rChkDate
            // 
            this.rChkDate.AutoSize = true;
            this.rChkDate.Checked = true;
            this.rChkDate.Location = new System.Drawing.Point(9, 27);
            this.rChkDate.Name = "rChkDate";
            this.rChkDate.Size = new System.Drawing.Size(104, 23);
            this.rChkDate.TabIndex = 0;
            this.rChkDate.TabStop = true;
            this.rChkDate.Text = "Date Wise";
            this.rChkDate.UseVisualStyleBackColor = true;
            this.rChkDate.CheckedChanged += new System.EventHandler(this.rChkDate_CheckedChanged);
            // 
            // rChkInvoice
            // 
            this.rChkInvoice.AutoSize = true;
            this.rChkInvoice.Location = new System.Drawing.Point(9, 52);
            this.rChkInvoice.Name = "rChkInvoice";
            this.rChkInvoice.Size = new System.Drawing.Size(120, 23);
            this.rChkInvoice.TabIndex = 1;
            this.rChkInvoice.Text = "Invoice Wise";
            this.rChkInvoice.UseVisualStyleBackColor = true;
            // 
            // rChkProductGroup
            // 
            this.rChkProductGroup.AutoSize = true;
            this.rChkProductGroup.Location = new System.Drawing.Point(9, 127);
            this.rChkProductGroup.Name = "rChkProductGroup";
            this.rChkProductGroup.Size = new System.Drawing.Size(113, 23);
            this.rChkProductGroup.TabIndex = 3;
            this.rChkProductGroup.Text = "Group Wise";
            this.rChkProductGroup.UseVisualStyleBackColor = true;
            // 
            // rChkDepartment
            // 
            this.rChkDepartment.AutoSize = true;
            this.rChkDepartment.Location = new System.Drawing.Point(136, 50);
            this.rChkDepartment.Name = "rChkDepartment";
            this.rChkDepartment.Size = new System.Drawing.Size(158, 23);
            this.rChkDepartment.TabIndex = 5;
            this.rChkDepartment.Text = "Department Wise";
            this.rChkDepartment.UseVisualStyleBackColor = true;
            // 
            // rChkProduct
            // 
            this.rChkProduct.AutoSize = true;
            this.rChkProduct.Location = new System.Drawing.Point(9, 102);
            this.rChkProduct.Name = "rChkProduct";
            this.rChkProduct.Size = new System.Drawing.Size(125, 23);
            this.rChkProduct.TabIndex = 4;
            this.rChkProduct.Text = "Product Wise";
            this.rChkProduct.UseVisualStyleBackColor = true;
            // 
            // rChkVendor
            // 
            this.rChkVendor.AutoSize = true;
            this.rChkVendor.Location = new System.Drawing.Point(9, 77);
            this.rChkVendor.Name = "rChkVendor";
            this.rChkVendor.Size = new System.Drawing.Size(122, 23);
            this.rChkVendor.TabIndex = 2;
            this.rChkVendor.Text = "Vendor Wise";
            this.rChkVendor.UseVisualStyleBackColor = true;
            // 
            // rChkProductSubGroup
            // 
            this.rChkProductSubGroup.AutoSize = true;
            this.rChkProductSubGroup.Location = new System.Drawing.Point(136, 27);
            this.rChkProductSubGroup.Name = "rChkProductSubGroup";
            this.rChkProductSubGroup.Size = new System.Drawing.Size(141, 23);
            this.rChkProductSubGroup.TabIndex = 6;
            this.rChkProductSubGroup.Text = "SubGroup Wise";
            this.rChkProductSubGroup.UseVisualStyleBackColor = true;
            // 
            // rChkAgent
            // 
            this.rChkAgent.AutoSize = true;
            this.rChkAgent.Location = new System.Drawing.Point(136, 96);
            this.rChkAgent.Name = "rChkAgent";
            this.rChkAgent.Size = new System.Drawing.Size(111, 23);
            this.rChkAgent.TabIndex = 8;
            this.rChkAgent.Text = "Agent Wise";
            this.rChkAgent.UseVisualStyleBackColor = true;
            // 
            // rChkArea
            // 
            this.rChkArea.AutoSize = true;
            this.rChkArea.Location = new System.Drawing.Point(136, 73);
            this.rChkArea.Name = "rChkArea";
            this.rChkArea.Size = new System.Drawing.Size(103, 23);
            this.rChkArea.TabIndex = 7;
            this.rChkArea.Text = "Area Wise";
            this.rChkArea.UseVisualStyleBackColor = true;
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.rChkNormal);
            this.mrGroup1.Controls.Add(this.rChkOtherSales);
            this.mrGroup1.Controls.Add(this.rChkCreditSales);
            this.mrGroup1.Controls.Add(this.rChkAllType);
            this.mrGroup1.Controls.Add(this.rChkCashSales);
            this.mrGroup1.Controls.Add(this.rChkCardSales);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "Voucher Type";
            this.mrGroup1.Location = new System.Drawing.Point(310, 3);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 10;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(233, 87);
            this.mrGroup1.TabIndex = 1;
            // 
            // rChkNormal
            // 
            this.rChkNormal.AutoSize = true;
            this.rChkNormal.Checked = true;
            this.rChkNormal.Location = new System.Drawing.Point(4, 29);
            this.rChkNormal.Name = "rChkNormal";
            this.rChkNormal.Size = new System.Drawing.Size(81, 23);
            this.rChkNormal.TabIndex = 0;
            this.rChkNormal.TabStop = true;
            this.rChkNormal.Text = "Normal";
            this.rChkNormal.UseVisualStyleBackColor = true;
            // 
            // rChkOtherSales
            // 
            this.rChkOtherSales.AutoSize = true;
            this.rChkOtherSales.Location = new System.Drawing.Point(159, 57);
            this.rChkOtherSales.Name = "rChkOtherSales";
            this.rChkOtherSales.Size = new System.Drawing.Size(71, 23);
            this.rChkOtherSales.TabIndex = 5;
            this.rChkOtherSales.Text = "Other";
            this.rChkOtherSales.UseVisualStyleBackColor = true;
            // 
            // rChkCreditSales
            // 
            this.rChkCreditSales.AutoSize = true;
            this.rChkCreditSales.Location = new System.Drawing.Point(85, 29);
            this.rChkCreditSales.Name = "rChkCreditSales";
            this.rChkCreditSales.Size = new System.Drawing.Size(74, 23);
            this.rChkCreditSales.TabIndex = 2;
            this.rChkCreditSales.Text = "Credit";
            this.rChkCreditSales.UseVisualStyleBackColor = true;
            // 
            // rChkAllType
            // 
            this.rChkAllType.Location = new System.Drawing.Point(4, 57);
            this.rChkAllType.Name = "rChkAllType";
            this.rChkAllType.Size = new System.Drawing.Size(47, 23);
            this.rChkAllType.TabIndex = 1;
            this.rChkAllType.Text = "All";
            this.rChkAllType.UseVisualStyleBackColor = true;
            // 
            // rChkCashSales
            // 
            this.rChkCashSales.AutoSize = true;
            this.rChkCashSales.Location = new System.Drawing.Point(159, 29);
            this.rChkCashSales.Name = "rChkCashSales";
            this.rChkCashSales.Size = new System.Drawing.Size(65, 23);
            this.rChkCashSales.TabIndex = 4;
            this.rChkCashSales.Text = "Cash";
            this.rChkCashSales.UseVisualStyleBackColor = true;
            // 
            // rChkCardSales
            // 
            this.rChkCardSales.AutoSize = true;
            this.rChkCardSales.Location = new System.Drawing.Point(85, 57);
            this.rChkCardSales.Name = "rChkCardSales";
            this.rChkCardSales.Size = new System.Drawing.Size(63, 23);
            this.rChkCardSales.TabIndex = 3;
            this.rChkCardSales.Text = "Card";
            this.rChkCardSales.UseVisualStyleBackColor = true;
            // 
            // mrGroup2
            // 
            this.mrGroup2.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup2.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup2.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup2.BorderColor = System.Drawing.Color.White;
            this.mrGroup2.BorderThickness = 1F;
            this.mrGroup2.Controls.Add(this.MskFrom);
            this.mrGroup2.Controls.Add(this.MskToDate);
            this.mrGroup2.Controls.Add(this.CmbDateType);
            this.mrGroup2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup2.GroupImage = null;
            this.mrGroup2.GroupTitle = "Date Filter";
            this.mrGroup2.Location = new System.Drawing.Point(3, 3);
            this.mrGroup2.Name = "mrGroup2";
            this.mrGroup2.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup2.PaintGroupBox = false;
            this.mrGroup2.RoundCorners = 10;
            this.mrGroup2.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup2.ShadowControl = false;
            this.mrGroup2.ShadowThickness = 3;
            this.mrGroup2.Size = new System.Drawing.Size(305, 87);
            this.mrGroup2.TabIndex = 0;
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskFrom.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskFrom.Location = new System.Drawing.Point(9, 59);
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
            this.MskToDate.Location = new System.Drawing.Point(139, 59);
            this.MskToDate.Mask = "00/00/0000";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.Size = new System.Drawing.Size(129, 25);
            this.MskToDate.TabIndex = 2;
            this.MskToDate.Validated += new System.EventHandler(this.MskToDate_Validated);
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
            this.CmbDateType.Location = new System.Drawing.Point(9, 29);
            this.CmbDateType.Name = "CmbDateType";
            this.CmbDateType.Size = new System.Drawing.Size(289, 27);
            this.CmbDateType.TabIndex = 0;
            this.CmbDateType.SelectedIndexChanged += new System.EventHandler(this.CmbDateType_SelectedIndexChanged);
            // 
            // mrGroup5
            // 
            this.mrGroup5.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup5.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup5.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup5.BorderColor = System.Drawing.Color.White;
            this.mrGroup5.BorderThickness = 1F;
            this.mrGroup5.Controls.Add(this.ChkRefVno);
            this.mrGroup5.Controls.Add(this.ChkSummary);
            this.mrGroup5.Controls.Add(this.clsSeparator1);
            this.mrGroup5.Controls.Add(this.ChkIncludeAltQty);
            this.mrGroup5.Controls.Add(this.ChkIncludeGodown);
            this.mrGroup5.Controls.Add(this.ChkFreeQty);
            this.mrGroup5.Controls.Add(this.BtnShow);
            this.mrGroup5.Controls.Add(this.ChkDateWise);
            this.mrGroup5.Controls.Add(this.BtnCancel);
            this.mrGroup5.Controls.Add(this.ChkIncludeRemarks);
            this.mrGroup5.Controls.Add(this.ChkDocAgent);
            this.mrGroup5.Controls.Add(this.ChkAdditionalTerm);
            this.mrGroup5.Controls.Add(this.chk_ViewDxReport);
            this.mrGroup5.Controls.Add(this.ChkHorizontal);
            this.mrGroup5.Controls.Add(this.ChkIncludeOrderNo);
            this.mrGroup5.Controls.Add(this.chk_SelectAll);
            this.mrGroup5.Controls.Add(this.ChkIncludeChallanNo);
            this.mrGroup5.Controls.Add(this.ChkIncludeBatch);
            this.mrGroup5.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup5.GroupImage = null;
            this.mrGroup5.GroupTitle = "Filter Value";
            this.mrGroup5.Location = new System.Drawing.Point(2, 243);
            this.mrGroup5.Name = "mrGroup5";
            this.mrGroup5.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup5.PaintGroupBox = false;
            this.mrGroup5.RoundCorners = 10;
            this.mrGroup5.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup5.ShadowControl = false;
            this.mrGroup5.ShadowThickness = 3;
            this.mrGroup5.Size = new System.Drawing.Size(541, 178);
            this.mrGroup5.TabIndex = 4;
            // 
            // ChkRefVno
            // 
            this.ChkRefVno.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkRefVno.Location = new System.Drawing.Point(360, 115);
            this.ChkRefVno.Name = "ChkRefVno";
            this.ChkRefVno.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkRefVno.Size = new System.Drawing.Size(162, 23);
            this.ChkRefVno.TabIndex = 14;
            this.ChkRefVno.Text = "Ref Vno";
            this.ChkRefVno.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkRefVno.UseVisualStyleBackColor = true;
            // 
            // ChkSummary
            // 
            this.ChkSummary.Checked = true;
            this.ChkSummary.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkSummary.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSummary.Location = new System.Drawing.Point(10, 23);
            this.ChkSummary.Name = "ChkSummary";
            this.ChkSummary.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSummary.Size = new System.Drawing.Size(165, 23);
            this.ChkSummary.TabIndex = 0;
            this.ChkSummary.Text = "Summary";
            this.ChkSummary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSummary.UseVisualStyleBackColor = true;
            this.ChkSummary.CheckedChanged += new System.EventHandler(this.ChkSummary_CheckedChanged);
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator1.Location = new System.Drawing.Point(9, 139);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(521, 2);
            this.clsSeparator1.TabIndex = 17;
            this.clsSeparator1.TabStop = false;
            // 
            // ChkIncludeAltQty
            // 
            this.ChkIncludeAltQty.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeAltQty.Location = new System.Drawing.Point(360, 92);
            this.ChkIncludeAltQty.Name = "ChkIncludeAltQty";
            this.ChkIncludeAltQty.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeAltQty.Size = new System.Drawing.Size(162, 23);
            this.ChkIncludeAltQty.TabIndex = 13;
            this.ChkIncludeAltQty.Text = "Alt Qty";
            this.ChkIncludeAltQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeAltQty.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeGodown
            // 
            this.ChkIncludeGodown.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeGodown.Location = new System.Drawing.Point(10, 46);
            this.ChkIncludeGodown.Name = "ChkIncludeGodown";
            this.ChkIncludeGodown.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeGodown.Size = new System.Drawing.Size(165, 23);
            this.ChkIncludeGodown.TabIndex = 1;
            this.ChkIncludeGodown.Text = "Godown";
            this.ChkIncludeGodown.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeGodown.UseVisualStyleBackColor = true;
            // 
            // ChkFreeQty
            // 
            this.ChkFreeQty.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkFreeQty.Location = new System.Drawing.Point(181, 92);
            this.ChkFreeQty.Name = "ChkFreeQty";
            this.ChkFreeQty.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkFreeQty.Size = new System.Drawing.Size(162, 23);
            this.ChkFreeQty.TabIndex = 8;
            this.ChkFreeQty.Text = "Free Qty";
            this.ChkFreeQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkFreeQty.UseVisualStyleBackColor = true;
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.BtnShow.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnShow.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.BtnShow.Location = new System.Drawing.Point(320, 142);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(95, 34);
            this.BtnShow.TabIndex = 15;
            this.BtnShow.Text = "&SHOW";
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // ChkDateWise
            // 
            this.ChkDateWise.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDateWise.Location = new System.Drawing.Point(10, 69);
            this.ChkDateWise.Name = "ChkDateWise";
            this.ChkDateWise.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDateWise.Size = new System.Drawing.Size(165, 23);
            this.ChkDateWise.TabIndex = 2;
            this.ChkDateWise.Text = "Date";
            this.ChkDateWise.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDateWise.UseVisualStyleBackColor = true;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.BtnCancel.Location = new System.Drawing.Point(417, 142);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(112, 34);
            this.BtnCancel.TabIndex = 16;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // ChkIncludeRemarks
            // 
            this.ChkIncludeRemarks.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeRemarks.Location = new System.Drawing.Point(10, 92);
            this.ChkIncludeRemarks.Name = "ChkIncludeRemarks";
            this.ChkIncludeRemarks.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeRemarks.Size = new System.Drawing.Size(165, 23);
            this.ChkIncludeRemarks.TabIndex = 3;
            this.ChkIncludeRemarks.Text = "Remarks";
            this.ChkIncludeRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeRemarks.UseVisualStyleBackColor = true;
            // 
            // ChkDocAgent
            // 
            this.ChkDocAgent.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDocAgent.Location = new System.Drawing.Point(360, 69);
            this.ChkDocAgent.Name = "ChkDocAgent";
            this.ChkDocAgent.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDocAgent.Size = new System.Drawing.Size(162, 23);
            this.ChkDocAgent.TabIndex = 12;
            this.ChkDocAgent.Text = "DocAgent";
            this.ChkDocAgent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDocAgent.UseVisualStyleBackColor = true;
            // 
            // ChkAdditionalTerm
            // 
            this.ChkAdditionalTerm.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAdditionalTerm.Location = new System.Drawing.Point(181, 23);
            this.ChkAdditionalTerm.Name = "ChkAdditionalTerm";
            this.ChkAdditionalTerm.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkAdditionalTerm.Size = new System.Drawing.Size(162, 23);
            this.ChkAdditionalTerm.TabIndex = 5;
            this.ChkAdditionalTerm.Text = "Additional Term";
            this.ChkAdditionalTerm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkAdditionalTerm.UseVisualStyleBackColor = true;
            // 
            // chk_ViewDxReport
            // 
            this.chk_ViewDxReport.AllowDrop = true;
            this.chk_ViewDxReport.Cursor = System.Windows.Forms.Cursors.Default;
            this.chk_ViewDxReport.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_ViewDxReport.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chk_ViewDxReport.Location = new System.Drawing.Point(10, 115);
            this.chk_ViewDxReport.Name = "chk_ViewDxReport";
            this.chk_ViewDxReport.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_ViewDxReport.Size = new System.Drawing.Size(165, 23);
            this.chk_ViewDxReport.TabIndex = 4;
            this.chk_ViewDxReport.Text = "Dynamic Report";
            this.chk_ViewDxReport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_ViewDxReport.UseMnemonic = false;
            this.chk_ViewDxReport.UseVisualStyleBackColor = true;
            // 
            // ChkHorizontal
            // 
            this.ChkHorizontal.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkHorizontal.Location = new System.Drawing.Point(360, 23);
            this.ChkHorizontal.Name = "ChkHorizontal";
            this.ChkHorizontal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkHorizontal.Size = new System.Drawing.Size(162, 23);
            this.ChkHorizontal.TabIndex = 10;
            this.ChkHorizontal.Text = "Horizontal";
            this.ChkHorizontal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkHorizontal.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeOrderNo
            // 
            this.ChkIncludeOrderNo.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeOrderNo.Location = new System.Drawing.Point(181, 46);
            this.ChkIncludeOrderNo.Name = "ChkIncludeOrderNo";
            this.ChkIncludeOrderNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeOrderNo.Size = new System.Drawing.Size(162, 23);
            this.ChkIncludeOrderNo.TabIndex = 6;
            this.ChkIncludeOrderNo.Text = "Order No";
            this.ChkIncludeOrderNo.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.ChkIncludeOrderNo.UseVisualStyleBackColor = true;
            // 
            // chk_SelectAll
            // 
            this.chk_SelectAll.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_SelectAll.Location = new System.Drawing.Point(181, 115);
            this.chk_SelectAll.Name = "chk_SelectAll";
            this.chk_SelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_SelectAll.Size = new System.Drawing.Size(162, 23);
            this.chk_SelectAll.TabIndex = 9;
            this.chk_SelectAll.Text = "Select All";
            this.chk_SelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_SelectAll.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeChallanNo
            // 
            this.ChkIncludeChallanNo.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeChallanNo.Location = new System.Drawing.Point(360, 46);
            this.ChkIncludeChallanNo.Name = "ChkIncludeChallanNo";
            this.ChkIncludeChallanNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeChallanNo.Size = new System.Drawing.Size(162, 23);
            this.ChkIncludeChallanNo.TabIndex = 11;
            this.ChkIncludeChallanNo.Text = "Challan No";
            this.ChkIncludeChallanNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeChallanNo.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeBatch
            // 
            this.ChkIncludeBatch.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeBatch.Location = new System.Drawing.Point(181, 69);
            this.ChkIncludeBatch.Name = "ChkIncludeBatch";
            this.ChkIncludeBatch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeBatch.Size = new System.Drawing.Size(162, 23);
            this.ChkIncludeBatch.TabIndex = 7;
            this.ChkIncludeBatch.Text = "Batch";
            this.ChkIncludeBatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeBatch.UseVisualStyleBackColor = true;
            // 
            // FrmPurchaseRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(545, 423);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmPurchaseRegister";
            this.Text = "PURCHASE INVOICE REGISTER";
            this.Load += new System.EventHandler(this.PurchaseInvoiceRegister_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PurchaseInvoiceRegister_KeyPress);
            this.panel1.ResumeLayout(false);
            this.mrGroup4.ResumeLayout(false);
            this.mrGroup4.PerformLayout();
            this.mrGroup3.ResumeLayout(false);
            this.mrGroup3.PerformLayout();
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.mrGroup2.ResumeLayout(false);
            this.mrGroup2.PerformLayout();
            this.mrGroup5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ClsSeparator clsSeparator1;
        private MrTextBox TxtVoucherNo;
        private System.Windows.Forms.Label lbl_Find;
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private System.Windows.Forms.CheckBox ChkDocAgent;
        private System.Windows.Forms.CheckBox chk_ViewDxReport;
        private System.Windows.Forms.CheckBox ChkSummary;
        private System.Windows.Forms.CheckBox chk_SelectAll;
        private System.Windows.Forms.RadioButton rChkDate;
        private System.Windows.Forms.CheckBox ChkIncludeRemarks;
        private System.Windows.Forms.CheckBox ChkDateWise;
        private System.Windows.Forms.CheckBox ChkFreeQty;
        private System.Windows.Forms.CheckBox ChkIncludeAltQty;
        private System.Windows.Forms.RadioButton rChkInvoice;
        private System.Windows.Forms.RadioButton rChkDepartment;
        private System.Windows.Forms.RadioButton rChkVendor;
        private System.Windows.Forms.RadioButton rChkAgent;
        private System.Windows.Forms.RadioButton rChkArea;
        private System.Windows.Forms.RadioButton rChkProductSubGroup;
        private System.Windows.Forms.RadioButton rChkProduct;
        private System.Windows.Forms.CheckBox ChkIncludeBatch;
        private System.Windows.Forms.CheckBox ChkIncludeChallanNo;
        private System.Windows.Forms.CheckBox ChkIncludeOrderNo;
        private System.Windows.Forms.CheckBox ChkHorizontal;
        private System.Windows.Forms.CheckBox ChkAdditionalTerm;
        private System.Windows.Forms.RadioButton rChkProductGroup;
        private System.Windows.Forms.ComboBox CmbDateType;
        private MrMaskedTextBox MskFrom;
        private MrMaskedTextBox MskToDate;
        private System.Windows.Forms.RadioButton rChkOtherSales;
        private System.Windows.Forms.RadioButton rChkAllType;
        private System.Windows.Forms.RadioButton rChkCardSales;
        private System.Windows.Forms.RadioButton rChkCreditSales;
        private System.Windows.Forms.RadioButton rChkCashSales;
        private System.Windows.Forms.RadioButton rChkReturnRegister;
        private System.Windows.Forms.RadioButton rChkCancelRegister;
        private System.Windows.Forms.RadioButton rChkNormalRegister;
        private System.Windows.Forms.CheckBox ChkIncludeGodown;
        private System.Windows.Forms.CheckBox ChkRefVno;
        private MrGroup mrGroup2;
        private MrGroup mrGroup1;
        private MrGroup mrGroup3;
        private MrGroup mrGroup4;
        private MrGroup mrGroup5;
        private System.Windows.Forms.RadioButton rChkNormal;
        private MrPanel panel1;
    }
}