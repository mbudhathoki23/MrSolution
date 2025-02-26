using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.Sales_Register
{
    partial class FrmSalesInvoiceEntryRegister
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSalesInvoiceEntryRegister));
            this.rChkInvoice = new System.Windows.Forms.RadioButton();
            this.rChkDepartment = new System.Windows.Forms.RadioButton();
            this.rChkCustomer = new System.Windows.Forms.RadioButton();
            this.rChkCancelRegister = new System.Windows.Forms.RadioButton();
            this.panel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.mrGroup3 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.rChkDocAgent = new System.Windows.Forms.RadioButton();
            this.mrGroup6 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.ChkAltQtyWise = new System.Windows.Forms.RadioButton();
            this.ChkQtyWise = new System.Windows.Forms.RadioButton();
            this.TxtVoucherNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_Find = new System.Windows.Forms.Label();
            this.mrGroup2 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.rChkPartialSales = new System.Windows.Forms.RadioButton();
            this.rChkNormal = new System.Windows.Forms.RadioButton();
            this.rChkOtherSales = new System.Windows.Forms.RadioButton();
            this.rChkCashSales = new System.Windows.Forms.RadioButton();
            this.rChkAllType = new System.Windows.Forms.RadioButton();
            this.rChkCreditSales = new System.Windows.Forms.RadioButton();
            this.rChkCardSales = new System.Windows.Forms.RadioButton();
            this.rChkProduct = new System.Windows.Forms.RadioButton();
            this.mrGroup4 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.rChkReturnRegister = new System.Windows.Forms.RadioButton();
            this.rChkNormalRegister = new System.Windows.Forms.RadioButton();
            this.rChkProductSubGroup = new System.Windows.Forms.RadioButton();
            this.rChkDate = new System.Windows.Forms.RadioButton();
            this.rChkProductGroup = new System.Windows.Forms.RadioButton();
            this.rChkUserWise = new System.Windows.Forms.RadioButton();
            this.rChkArea = new System.Windows.Forms.RadioButton();
            this.rChkAgent = new System.Windows.Forms.RadioButton();
            this.rChkCounter = new System.Windows.Forms.RadioButton();
            this.mrGroup1 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.CmbDateType = new System.Windows.Forms.ComboBox();
            this.MskFrom = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.MskToDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.mrGroup5 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.ChkIncludeAltQty = new System.Windows.Forms.CheckBox();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.ChkSummary = new System.Windows.Forms.CheckBox();
            this.btn_Show = new DevExpress.XtraEditors.SimpleButton();
            this.chk_ViewDxReport = new System.Windows.Forms.CheckBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.ChkIncludeGodown = new System.Windows.Forms.CheckBox();
            this.ChkFreeQty = new System.Windows.Forms.CheckBox();
            this.ChkDateWise = new System.Windows.Forms.CheckBox();
            this.ChkIncludeRemarks = new System.Windows.Forms.CheckBox();
            this.ChkSelectAll = new System.Windows.Forms.CheckBox();
            this.ChkAdditionalTerm = new System.Windows.Forms.CheckBox();
            this.ChkIncludeBatch = new System.Windows.Forms.CheckBox();
            this.ChkHorizontal = new System.Windows.Forms.CheckBox();
            this.ChkIncludeChallanNo = new System.Windows.Forms.CheckBox();
            this.ChkIncludeOrderNo = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.mrGroup3.SuspendLayout();
            this.mrGroup6.SuspendLayout();
            this.mrGroup2.SuspendLayout();
            this.mrGroup4.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            this.mrGroup5.SuspendLayout();
            this.SuspendLayout();
            // 
            // rChkInvoice
            // 
            this.rChkInvoice.AutoSize = true;
            this.rChkInvoice.Location = new System.Drawing.Point(10, 53);
            this.rChkInvoice.Name = "rChkInvoice";
            this.rChkInvoice.Size = new System.Drawing.Size(120, 23);
            this.rChkInvoice.TabIndex = 1;
            this.rChkInvoice.Text = "Invoice Wise";
            this.rChkInvoice.UseVisualStyleBackColor = true;
            this.rChkInvoice.CheckedChanged += new System.EventHandler(this.rChkDate_CheckedChanged);
            // 
            // rChkDepartment
            // 
            this.rChkDepartment.AutoSize = true;
            this.rChkDepartment.Location = new System.Drawing.Point(169, 30);
            this.rChkDepartment.Name = "rChkDepartment";
            this.rChkDepartment.Size = new System.Drawing.Size(158, 23);
            this.rChkDepartment.TabIndex = 4;
            this.rChkDepartment.Text = "Department Wise";
            this.rChkDepartment.UseVisualStyleBackColor = true;
            // 
            // rChkCustomer
            // 
            this.rChkCustomer.AutoSize = true;
            this.rChkCustomer.Location = new System.Drawing.Point(10, 75);
            this.rChkCustomer.Name = "rChkCustomer";
            this.rChkCustomer.Size = new System.Drawing.Size(141, 23);
            this.rChkCustomer.TabIndex = 2;
            this.rChkCustomer.Text = "Customer Wise";
            this.rChkCustomer.UseVisualStyleBackColor = true;
            // 
            // rChkCancelRegister
            // 
            this.rChkCancelRegister.AutoSize = true;
            this.rChkCancelRegister.Location = new System.Drawing.Point(312, 25);
            this.rChkCancelRegister.Name = "rChkCancelRegister";
            this.rChkCancelRegister.Size = new System.Drawing.Size(147, 23);
            this.rChkCancelRegister.TabIndex = 2;
            this.rChkCancelRegister.Text = "Cancel Register";
            this.rChkCancelRegister.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.mrGroup3);
            this.panel1.Controls.Add(this.mrGroup1);
            this.panel1.Controls.Add(this.mrGroup5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(528, 504);
            this.panel1.TabIndex = 0;
            // 
            // mrGroup3
            // 
            this.mrGroup3.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup3.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup3.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup3.BorderColor = System.Drawing.Color.White;
            this.mrGroup3.BorderThickness = 1F;
            this.mrGroup3.Controls.Add(this.rChkDocAgent);
            this.mrGroup3.Controls.Add(this.mrGroup6);
            this.mrGroup3.Controls.Add(this.TxtVoucherNo);
            this.mrGroup3.Controls.Add(this.lbl_Find);
            this.mrGroup3.Controls.Add(this.mrGroup2);
            this.mrGroup3.Controls.Add(this.rChkProduct);
            this.mrGroup3.Controls.Add(this.mrGroup4);
            this.mrGroup3.Controls.Add(this.rChkProductSubGroup);
            this.mrGroup3.Controls.Add(this.rChkDate);
            this.mrGroup3.Controls.Add(this.rChkProductGroup);
            this.mrGroup3.Controls.Add(this.rChkInvoice);
            this.mrGroup3.Controls.Add(this.rChkDepartment);
            this.mrGroup3.Controls.Add(this.rChkUserWise);
            this.mrGroup3.Controls.Add(this.rChkArea);
            this.mrGroup3.Controls.Add(this.rChkAgent);
            this.mrGroup3.Controls.Add(this.rChkCounter);
            this.mrGroup3.Controls.Add(this.rChkCustomer);
            this.mrGroup3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup3.GroupImage = null;
            this.mrGroup3.GroupTitle = "Report Type";
            this.mrGroup3.Location = new System.Drawing.Point(3, 65);
            this.mrGroup3.Name = "mrGroup3";
            this.mrGroup3.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.mrGroup3.PaintGroupBox = false;
            this.mrGroup3.RoundCorners = 10;
            this.mrGroup3.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup3.ShadowControl = false;
            this.mrGroup3.ShadowThickness = 3;
            this.mrGroup3.Size = new System.Drawing.Size(521, 295);
            this.mrGroup3.TabIndex = 1;
            // 
            // rChkDocAgent
            // 
            this.rChkDocAgent.AutoSize = true;
            this.rChkDocAgent.Location = new System.Drawing.Point(169, 96);
            this.rChkDocAgent.Name = "rChkDocAgent";
            this.rChkDocAgent.Size = new System.Drawing.Size(144, 23);
            this.rChkDocAgent.TabIndex = 24;
            this.rChkDocAgent.Text = "Doc Agent Wise";
            this.rChkDocAgent.UseVisualStyleBackColor = true;
            // 
            // mrGroup6
            // 
            this.mrGroup6.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup6.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup6.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup6.BorderColor = System.Drawing.Color.White;
            this.mrGroup6.BorderThickness = 1F;
            this.mrGroup6.Controls.Add(this.ChkAltQtyWise);
            this.mrGroup6.Controls.Add(this.ChkQtyWise);
            this.mrGroup6.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup6.GroupImage = null;
            this.mrGroup6.GroupTitle = "Qty Type";
            this.mrGroup6.Location = new System.Drawing.Point(5, 241);
            this.mrGroup6.Name = "mrGroup6";
            this.mrGroup6.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.mrGroup6.PaintGroupBox = false;
            this.mrGroup6.RoundCorners = 10;
            this.mrGroup6.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup6.ShadowControl = false;
            this.mrGroup6.ShadowThickness = 3;
            this.mrGroup6.Size = new System.Drawing.Size(218, 52);
            this.mrGroup6.TabIndex = 5;
            // 
            // ChkAltQtyWise
            // 
            this.ChkAltQtyWise.AutoSize = true;
            this.ChkAltQtyWise.Location = new System.Drawing.Point(106, 24);
            this.ChkAltQtyWise.Name = "ChkAltQtyWise";
            this.ChkAltQtyWise.Size = new System.Drawing.Size(106, 23);
            this.ChkAltQtyWise.TabIndex = 6;
            this.ChkAltQtyWise.Text = "On Alt Qty";
            this.ChkAltQtyWise.UseVisualStyleBackColor = true;
            // 
            // ChkQtyWise
            // 
            this.ChkQtyWise.AutoSize = true;
            this.ChkQtyWise.Checked = true;
            this.ChkQtyWise.Location = new System.Drawing.Point(7, 24);
            this.ChkQtyWise.Name = "ChkQtyWise";
            this.ChkQtyWise.Size = new System.Drawing.Size(85, 23);
            this.ChkQtyWise.TabIndex = 6;
            this.ChkQtyWise.TabStop = true;
            this.ChkQtyWise.Text = "On Qty ";
            this.ChkQtyWise.UseVisualStyleBackColor = true;
            this.ChkQtyWise.CheckedChanged += new System.EventHandler(this.ChkQtyWise_CheckedChanged);
            // 
            // TxtVoucherNo
            // 
            this.TxtVoucherNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtVoucherNo.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtVoucherNo.Location = new System.Drawing.Point(338, 259);
            this.TxtVoucherNo.Name = "TxtVoucherNo";
            this.TxtVoucherNo.Size = new System.Drawing.Size(176, 25);
            this.TxtVoucherNo.TabIndex = 3;
            // 
            // lbl_Find
            // 
            this.lbl_Find.AutoSize = true;
            this.lbl_Find.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Find.Location = new System.Drawing.Point(232, 261);
            this.lbl_Find.Name = "lbl_Find";
            this.lbl_Find.Size = new System.Drawing.Size(100, 19);
            this.lbl_Find.TabIndex = 23;
            this.lbl_Find.Text = "Find Invoice";
            // 
            // mrGroup2
            // 
            this.mrGroup2.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup2.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup2.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup2.BorderColor = System.Drawing.Color.White;
            this.mrGroup2.BorderThickness = 1F;
            this.mrGroup2.Controls.Add(this.rChkPartialSales);
            this.mrGroup2.Controls.Add(this.rChkNormal);
            this.mrGroup2.Controls.Add(this.rChkOtherSales);
            this.mrGroup2.Controls.Add(this.rChkCashSales);
            this.mrGroup2.Controls.Add(this.rChkAllType);
            this.mrGroup2.Controls.Add(this.rChkCreditSales);
            this.mrGroup2.Controls.Add(this.rChkCardSales);
            this.mrGroup2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup2.GroupImage = null;
            this.mrGroup2.GroupTitle = "Type";
            this.mrGroup2.Location = new System.Drawing.Point(5, 119);
            this.mrGroup2.Name = "mrGroup2";
            this.mrGroup2.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.mrGroup2.PaintGroupBox = false;
            this.mrGroup2.RoundCorners = 10;
            this.mrGroup2.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup2.ShadowControl = false;
            this.mrGroup2.ShadowThickness = 3;
            this.mrGroup2.Size = new System.Drawing.Size(512, 60);
            this.mrGroup2.TabIndex = 11;
            // 
            // rChkPartialSales
            // 
            this.rChkPartialSales.AutoSize = true;
            this.rChkPartialSales.Location = new System.Drawing.Point(436, 26);
            this.rChkPartialSales.Name = "rChkPartialSales";
            this.rChkPartialSales.Size = new System.Drawing.Size(77, 23);
            this.rChkPartialSales.TabIndex = 5;
            this.rChkPartialSales.Text = "Partial";
            this.rChkPartialSales.UseVisualStyleBackColor = true;
            // 
            // rChkNormal
            // 
            this.rChkNormal.AutoSize = true;
            this.rChkNormal.Checked = true;
            this.rChkNormal.Location = new System.Drawing.Point(5, 26);
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
            this.rChkOtherSales.Location = new System.Drawing.Point(360, 26);
            this.rChkOtherSales.Name = "rChkOtherSales";
            this.rChkOtherSales.Size = new System.Drawing.Size(71, 23);
            this.rChkOtherSales.TabIndex = 4;
            this.rChkOtherSales.Text = "Other";
            this.rChkOtherSales.UseVisualStyleBackColor = true;
            // 
            // rChkCashSales
            // 
            this.rChkCashSales.AutoSize = true;
            this.rChkCashSales.Location = new System.Drawing.Point(143, 26);
            this.rChkCashSales.Name = "rChkCashSales";
            this.rChkCashSales.Size = new System.Drawing.Size(65, 23);
            this.rChkCashSales.TabIndex = 3;
            this.rChkCashSales.Text = "Cash";
            this.rChkCashSales.UseVisualStyleBackColor = true;
            // 
            // rChkAllType
            // 
            this.rChkAllType.AutoSize = true;
            this.rChkAllType.Location = new System.Drawing.Point(91, 26);
            this.rChkAllType.Name = "rChkAllType";
            this.rChkAllType.Size = new System.Drawing.Size(47, 23);
            this.rChkAllType.TabIndex = 1;
            this.rChkAllType.Text = "All";
            this.rChkAllType.UseVisualStyleBackColor = true;
            // 
            // rChkCreditSales
            // 
            this.rChkCreditSales.AutoSize = true;
            this.rChkCreditSales.Location = new System.Drawing.Point(213, 26);
            this.rChkCreditSales.Name = "rChkCreditSales";
            this.rChkCreditSales.Size = new System.Drawing.Size(74, 23);
            this.rChkCreditSales.TabIndex = 2;
            this.rChkCreditSales.Text = "Credit";
            this.rChkCreditSales.UseVisualStyleBackColor = true;
            // 
            // rChkCardSales
            // 
            this.rChkCardSales.AutoSize = true;
            this.rChkCardSales.Location = new System.Drawing.Point(292, 26);
            this.rChkCardSales.Name = "rChkCardSales";
            this.rChkCardSales.Size = new System.Drawing.Size(63, 23);
            this.rChkCardSales.TabIndex = 3;
            this.rChkCardSales.Text = "Card";
            this.rChkCardSales.UseVisualStyleBackColor = true;
            // 
            // rChkProduct
            // 
            this.rChkProduct.AutoSize = true;
            this.rChkProduct.Location = new System.Drawing.Point(360, 30);
            this.rChkProduct.Name = "rChkProduct";
            this.rChkProduct.Size = new System.Drawing.Size(125, 23);
            this.rChkProduct.TabIndex = 8;
            this.rChkProduct.Text = "Product Wise";
            this.rChkProduct.UseVisualStyleBackColor = true;
            this.rChkProduct.CheckedChanged += new System.EventHandler(this.rChkDate_CheckedChanged);
            // 
            // mrGroup4
            // 
            this.mrGroup4.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup4.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup4.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup4.BorderColor = System.Drawing.Color.White;
            this.mrGroup4.BorderThickness = 1F;
            this.mrGroup4.Controls.Add(this.rChkReturnRegister);
            this.mrGroup4.Controls.Add(this.rChkNormalRegister);
            this.mrGroup4.Controls.Add(this.rChkCancelRegister);
            this.mrGroup4.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup4.GroupImage = null;
            this.mrGroup4.GroupTitle = "Category";
            this.mrGroup4.Location = new System.Drawing.Point(5, 181);
            this.mrGroup4.Name = "mrGroup4";
            this.mrGroup4.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.mrGroup4.PaintGroupBox = false;
            this.mrGroup4.RoundCorners = 10;
            this.mrGroup4.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup4.ShadowControl = false;
            this.mrGroup4.ShadowThickness = 3;
            this.mrGroup4.Size = new System.Drawing.Size(513, 58);
            this.mrGroup4.TabIndex = 12;
            // 
            // rChkReturnRegister
            // 
            this.rChkReturnRegister.AutoSize = true;
            this.rChkReturnRegister.Location = new System.Drawing.Point(158, 25);
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
            this.rChkNormalRegister.Location = new System.Drawing.Point(6, 25);
            this.rChkNormalRegister.Name = "rChkNormalRegister";
            this.rChkNormalRegister.Size = new System.Drawing.Size(149, 23);
            this.rChkNormalRegister.TabIndex = 0;
            this.rChkNormalRegister.TabStop = true;
            this.rChkNormalRegister.Text = "Normal Register";
            this.rChkNormalRegister.UseVisualStyleBackColor = true;
            // 
            // rChkProductSubGroup
            // 
            this.rChkProductSubGroup.AutoSize = true;
            this.rChkProductSubGroup.Location = new System.Drawing.Point(360, 75);
            this.rChkProductSubGroup.Name = "rChkProductSubGroup";
            this.rChkProductSubGroup.Size = new System.Drawing.Size(141, 23);
            this.rChkProductSubGroup.TabIndex = 10;
            this.rChkProductSubGroup.Text = "SubGroup Wise";
            this.rChkProductSubGroup.UseVisualStyleBackColor = true;
            this.rChkProductSubGroup.CheckedChanged += new System.EventHandler(this.rChkDate_CheckedChanged);
            // 
            // rChkDate
            // 
            this.rChkDate.AutoSize = true;
            this.rChkDate.Checked = true;
            this.rChkDate.Location = new System.Drawing.Point(10, 30);
            this.rChkDate.Name = "rChkDate";
            this.rChkDate.Size = new System.Drawing.Size(104, 23);
            this.rChkDate.TabIndex = 0;
            this.rChkDate.TabStop = true;
            this.rChkDate.Text = "Date Wise";
            this.rChkDate.UseVisualStyleBackColor = true;
            this.rChkDate.CheckedChanged += new System.EventHandler(this.rChkDate_CheckedChanged);
            // 
            // rChkProductGroup
            // 
            this.rChkProductGroup.AutoSize = true;
            this.rChkProductGroup.Location = new System.Drawing.Point(360, 53);
            this.rChkProductGroup.Name = "rChkProductGroup";
            this.rChkProductGroup.Size = new System.Drawing.Size(113, 23);
            this.rChkProductGroup.TabIndex = 9;
            this.rChkProductGroup.Text = "Group Wise";
            this.rChkProductGroup.UseVisualStyleBackColor = true;
            this.rChkProductGroup.CheckedChanged += new System.EventHandler(this.rChkDate_CheckedChanged);
            // 
            // rChkUserWise
            // 
            this.rChkUserWise.AutoSize = true;
            this.rChkUserWise.Location = new System.Drawing.Point(169, 75);
            this.rChkUserWise.Name = "rChkUserWise";
            this.rChkUserWise.Size = new System.Drawing.Size(104, 23);
            this.rChkUserWise.TabIndex = 6;
            this.rChkUserWise.Text = "User Wise";
            this.rChkUserWise.UseVisualStyleBackColor = true;
            this.rChkUserWise.CheckedChanged += new System.EventHandler(this.rChkDate_CheckedChanged);
            // 
            // rChkArea
            // 
            this.rChkArea.AutoSize = true;
            this.rChkArea.Location = new System.Drawing.Point(10, 97);
            this.rChkArea.Name = "rChkArea";
            this.rChkArea.Size = new System.Drawing.Size(103, 23);
            this.rChkArea.TabIndex = 3;
            this.rChkArea.Text = "Area Wise";
            this.rChkArea.UseVisualStyleBackColor = true;
            // 
            // rChkAgent
            // 
            this.rChkAgent.AutoSize = true;
            this.rChkAgent.Location = new System.Drawing.Point(169, 53);
            this.rChkAgent.Name = "rChkAgent";
            this.rChkAgent.Size = new System.Drawing.Size(111, 23);
            this.rChkAgent.TabIndex = 5;
            this.rChkAgent.Text = "Agent Wise";
            this.rChkAgent.UseVisualStyleBackColor = true;
            // 
            // rChkCounter
            // 
            this.rChkCounter.AutoSize = true;
            this.rChkCounter.Location = new System.Drawing.Point(360, 97);
            this.rChkCounter.Name = "rChkCounter";
            this.rChkCounter.Size = new System.Drawing.Size(129, 23);
            this.rChkCounter.TabIndex = 7;
            this.rChkCounter.Text = "Counter Wise";
            this.rChkCounter.UseVisualStyleBackColor = true;
            this.rChkCounter.CheckedChanged += new System.EventHandler(this.rChkDate_CheckedChanged);
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.CmbDateType);
            this.mrGroup1.Controls.Add(this.MskFrom);
            this.mrGroup1.Controls.Add(this.MskToDate);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "Date Filter";
            this.mrGroup1.Location = new System.Drawing.Point(3, 3);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 10;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(521, 58);
            this.mrGroup1.TabIndex = 0;
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
            this.CmbDateType.Location = new System.Drawing.Point(5, 27);
            this.CmbDateType.Name = "CmbDateType";
            this.CmbDateType.Size = new System.Drawing.Size(237, 27);
            this.CmbDateType.TabIndex = 0;
            this.CmbDateType.SelectedIndexChanged += new System.EventHandler(this.CmbDateType_SelectedIndexChanged);
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskFrom.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskFrom.Location = new System.Drawing.Point(244, 29);
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
            this.MskToDate.Location = new System.Drawing.Point(374, 29);
            this.MskToDate.Mask = "00/00/0000";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.Size = new System.Drawing.Size(129, 25);
            this.MskToDate.TabIndex = 2;
            this.MskToDate.Validated += new System.EventHandler(this.MskToDate_Validated);
            // 
            // mrGroup5
            // 
            this.mrGroup5.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup5.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup5.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup5.BorderColor = System.Drawing.Color.White;
            this.mrGroup5.BorderThickness = 1F;
            this.mrGroup5.Controls.Add(this.ChkIncludeAltQty);
            this.mrGroup5.Controls.Add(this.clsSeparator1);
            this.mrGroup5.Controls.Add(this.ChkSummary);
            this.mrGroup5.Controls.Add(this.btn_Show);
            this.mrGroup5.Controls.Add(this.chk_ViewDxReport);
            this.mrGroup5.Controls.Add(this.BtnCancel);
            this.mrGroup5.Controls.Add(this.ChkIncludeGodown);
            this.mrGroup5.Controls.Add(this.ChkFreeQty);
            this.mrGroup5.Controls.Add(this.ChkDateWise);
            this.mrGroup5.Controls.Add(this.ChkIncludeRemarks);
            this.mrGroup5.Controls.Add(this.ChkSelectAll);
            this.mrGroup5.Controls.Add(this.ChkAdditionalTerm);
            this.mrGroup5.Controls.Add(this.ChkIncludeBatch);
            this.mrGroup5.Controls.Add(this.ChkHorizontal);
            this.mrGroup5.Controls.Add(this.ChkIncludeChallanNo);
            this.mrGroup5.Controls.Add(this.ChkIncludeOrderNo);
            this.mrGroup5.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup5.GroupImage = null;
            this.mrGroup5.GroupTitle = "";
            this.mrGroup5.Location = new System.Drawing.Point(3, 351);
            this.mrGroup5.Name = "mrGroup5";
            this.mrGroup5.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.mrGroup5.PaintGroupBox = false;
            this.mrGroup5.RoundCorners = 5;
            this.mrGroup5.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup5.ShadowControl = false;
            this.mrGroup5.ShadowThickness = 3;
            this.mrGroup5.Size = new System.Drawing.Size(521, 147);
            this.mrGroup5.TabIndex = 4;
            // 
            // ChkIncludeAltQty
            // 
            this.ChkIncludeAltQty.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeAltQty.Location = new System.Drawing.Point(356, 61);
            this.ChkIncludeAltQty.Name = "ChkIncludeAltQty";
            this.ChkIncludeAltQty.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeAltQty.Size = new System.Drawing.Size(147, 23);
            this.ChkIncludeAltQty.TabIndex = 16;
            this.ChkIncludeAltQty.Text = "Alt Qty";
            this.ChkIncludeAltQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeAltQty.UseVisualStyleBackColor = true;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(1, 107);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(507, 2);
            this.clsSeparator1.TabIndex = 12;
            this.clsSeparator1.TabStop = false;
            // 
            // ChkSummary
            // 
            this.ChkSummary.Checked = true;
            this.ChkSummary.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkSummary.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSummary.Location = new System.Drawing.Point(4, 15);
            this.ChkSummary.Name = "ChkSummary";
            this.ChkSummary.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSummary.Size = new System.Drawing.Size(165, 23);
            this.ChkSummary.TabIndex = 0;
            this.ChkSummary.Text = "Summary";
            this.ChkSummary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSummary.UseVisualStyleBackColor = true;
            this.ChkSummary.CheckedChanged += new System.EventHandler(this.ChkSummary_CheckedChanged);
            // 
            // btn_Show
            // 
            this.btn_Show.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Show.Appearance.Options.UseFont = true;
            this.btn_Show.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.btn_Show.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.btn_Show.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.btn_Show.Location = new System.Drawing.Point(286, 110);
            this.btn_Show.Name = "btn_Show";
            this.btn_Show.Size = new System.Drawing.Size(95, 34);
            this.btn_Show.TabIndex = 14;
            this.btn_Show.Text = "&SHOW";
            this.btn_Show.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // chk_ViewDxReport
            // 
            this.chk_ViewDxReport.AllowDrop = true;
            this.chk_ViewDxReport.Cursor = System.Windows.Forms.Cursors.Default;
            this.chk_ViewDxReport.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_ViewDxReport.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chk_ViewDxReport.Location = new System.Drawing.Point(4, 115);
            this.chk_ViewDxReport.Name = "chk_ViewDxReport";
            this.chk_ViewDxReport.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_ViewDxReport.Size = new System.Drawing.Size(165, 23);
            this.chk_ViewDxReport.TabIndex = 4;
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
            this.BtnCancel.Location = new System.Drawing.Point(383, 110);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(112, 34);
            this.BtnCancel.TabIndex = 15;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // ChkIncludeGodown
            // 
            this.ChkIncludeGodown.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeGodown.Location = new System.Drawing.Point(4, 38);
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
            this.ChkFreeQty.Location = new System.Drawing.Point(175, 84);
            this.ChkFreeQty.Name = "ChkFreeQty";
            this.ChkFreeQty.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkFreeQty.Size = new System.Drawing.Size(162, 23);
            this.ChkFreeQty.TabIndex = 8;
            this.ChkFreeQty.Text = "Free Qty";
            this.ChkFreeQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkFreeQty.UseVisualStyleBackColor = true;
            // 
            // ChkDateWise
            // 
            this.ChkDateWise.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDateWise.Location = new System.Drawing.Point(4, 61);
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
            this.ChkIncludeRemarks.Location = new System.Drawing.Point(4, 84);
            this.ChkIncludeRemarks.Name = "ChkIncludeRemarks";
            this.ChkIncludeRemarks.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeRemarks.Size = new System.Drawing.Size(165, 23);
            this.ChkIncludeRemarks.TabIndex = 3;
            this.ChkIncludeRemarks.Text = "Remarks";
            this.ChkIncludeRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeRemarks.UseVisualStyleBackColor = true;
            // 
            // ChkSelectAll
            // 
            this.ChkSelectAll.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSelectAll.Location = new System.Drawing.Point(354, 84);
            this.ChkSelectAll.Name = "ChkSelectAll";
            this.ChkSelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSelectAll.Size = new System.Drawing.Size(149, 23);
            this.ChkSelectAll.TabIndex = 9;
            this.ChkSelectAll.Text = "Select All";
            this.ChkSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSelectAll.UseVisualStyleBackColor = true;
            // 
            // ChkAdditionalTerm
            // 
            this.ChkAdditionalTerm.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAdditionalTerm.Location = new System.Drawing.Point(175, 15);
            this.ChkAdditionalTerm.Name = "ChkAdditionalTerm";
            this.ChkAdditionalTerm.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkAdditionalTerm.Size = new System.Drawing.Size(162, 23);
            this.ChkAdditionalTerm.TabIndex = 5;
            this.ChkAdditionalTerm.Text = "Additional Term";
            this.ChkAdditionalTerm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkAdditionalTerm.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeBatch
            // 
            this.ChkIncludeBatch.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeBatch.Location = new System.Drawing.Point(175, 61);
            this.ChkIncludeBatch.Name = "ChkIncludeBatch";
            this.ChkIncludeBatch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeBatch.Size = new System.Drawing.Size(162, 23);
            this.ChkIncludeBatch.TabIndex = 7;
            this.ChkIncludeBatch.Text = "Batch";
            this.ChkIncludeBatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeBatch.UseVisualStyleBackColor = true;
            // 
            // ChkHorizontal
            // 
            this.ChkHorizontal.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkHorizontal.Location = new System.Drawing.Point(354, 15);
            this.ChkHorizontal.Name = "ChkHorizontal";
            this.ChkHorizontal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkHorizontal.Size = new System.Drawing.Size(149, 23);
            this.ChkHorizontal.TabIndex = 10;
            this.ChkHorizontal.Text = "Horizontal";
            this.ChkHorizontal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkHorizontal.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeChallanNo
            // 
            this.ChkIncludeChallanNo.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeChallanNo.Location = new System.Drawing.Point(354, 38);
            this.ChkIncludeChallanNo.Name = "ChkIncludeChallanNo";
            this.ChkIncludeChallanNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeChallanNo.Size = new System.Drawing.Size(149, 23);
            this.ChkIncludeChallanNo.TabIndex = 11;
            this.ChkIncludeChallanNo.Text = "Challan No";
            this.ChkIncludeChallanNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeChallanNo.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeOrderNo
            // 
            this.ChkIncludeOrderNo.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeOrderNo.Location = new System.Drawing.Point(175, 38);
            this.ChkIncludeOrderNo.Name = "ChkIncludeOrderNo";
            this.ChkIncludeOrderNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeOrderNo.Size = new System.Drawing.Size(162, 23);
            this.ChkIncludeOrderNo.TabIndex = 6;
            this.ChkIncludeOrderNo.Text = "Order No";
            this.ChkIncludeOrderNo.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.ChkIncludeOrderNo.UseVisualStyleBackColor = true;
            // 
            // FrmSalesInvoiceEntryRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(528, 504);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.Name = "FrmSalesInvoiceEntryRegister";
            this.ShowIcon = false;
            this.Text = "SALES INVOICE REGISTER REPORT";
            this.Load += new System.EventHandler(this.SalesInvoiceRegister_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SalesInvoiceRegister_KeyPress);
            this.panel1.ResumeLayout(false);
            this.mrGroup3.ResumeLayout(false);
            this.mrGroup3.PerformLayout();
            this.mrGroup6.ResumeLayout(false);
            this.mrGroup6.PerformLayout();
            this.mrGroup2.ResumeLayout(false);
            this.mrGroup2.PerformLayout();
            this.mrGroup4.ResumeLayout(false);
            this.mrGroup4.PerformLayout();
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.mrGroup5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rChkInvoice;
        private System.Windows.Forms.RadioButton rChkDepartment;
        private System.Windows.Forms.RadioButton rChkCustomer;
        private System.Windows.Forms.RadioButton rChkCancelRegister;
        private System.Windows.Forms.ComboBox CmbDateType;
        private System.Windows.Forms.RadioButton rChkNormal;
        private System.Windows.Forms.RadioButton rChkOtherSales;
        private System.Windows.Forms.RadioButton rChkAllType;
        private System.Windows.Forms.RadioButton rChkCardSales;
        private System.Windows.Forms.RadioButton rChkCreditSales;
        private System.Windows.Forms.RadioButton rChkCashSales;
        private System.Windows.Forms.RadioButton rChkDate;
        private System.Windows.Forms.RadioButton rChkUserWise;
        private System.Windows.Forms.RadioButton rChkAgent;
        private System.Windows.Forms.RadioButton rChkCounter;
        private System.Windows.Forms.RadioButton rChkArea;
        private System.Windows.Forms.RadioButton rChkProductSubGroup;
        private System.Windows.Forms.RadioButton rChkProduct;
        private System.Windows.Forms.RadioButton rChkProductGroup;
        private System.Windows.Forms.RadioButton rChkReturnRegister;
        private System.Windows.Forms.Label lbl_Find;
        private System.Windows.Forms.RadioButton rChkNormalRegister;
        private ClsSeparator clsSeparator1;
        private DevExpress.XtraEditors.SimpleButton btn_Show;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private System.Windows.Forms.CheckBox chk_ViewDxReport;
        private System.Windows.Forms.CheckBox ChkSummary;
        private System.Windows.Forms.CheckBox ChkSelectAll;
        private System.Windows.Forms.CheckBox ChkIncludeBatch;
        private System.Windows.Forms.CheckBox ChkIncludeChallanNo;
        private System.Windows.Forms.CheckBox ChkIncludeOrderNo;
        private System.Windows.Forms.CheckBox ChkHorizontal;
        private System.Windows.Forms.CheckBox ChkAdditionalTerm;
        private System.Windows.Forms.CheckBox ChkIncludeRemarks;
        private System.Windows.Forms.CheckBox ChkDateWise;
        private System.Windows.Forms.CheckBox ChkFreeQty;
        private System.Windows.Forms.CheckBox ChkIncludeGodown;
        private MrGroup mrGroup2;
        private MrGroup mrGroup1;
        private MrGroup mrGroup3;
        private MrGroup mrGroup4;
        private MrGroup mrGroup5;
        private MrPanel panel1;
        private MrMaskedTextBox MskFrom;
        private MrMaskedTextBox MskToDate;
        private MrTextBox TxtVoucherNo;
        private System.Windows.Forms.RadioButton rChkPartialSales;
        private MrGroup mrGroup6;
        private System.Windows.Forms.RadioButton ChkAltQtyWise;
        private System.Windows.Forms.RadioButton ChkQtyWise;
        private System.Windows.Forms.RadioButton rChkDocAgent;
        private System.Windows.Forms.CheckBox ChkIncludeAltQty;
    }
}