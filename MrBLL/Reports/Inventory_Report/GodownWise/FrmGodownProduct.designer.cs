using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Inventory_Report.GodownWise
{
    partial class FrmGodownProduct
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
            this.lbl_Branch = new System.Windows.Forms.Label();
            this.cmb_Branch = new System.Windows.Forms.ComboBox();
            this.chk_WithValue = new System.Windows.Forms.CheckBox();
            this.rb_ProductGodownWise = new System.Windows.Forms.RadioButton();
            this.rb_GodownProductWise = new System.Windows.Forms.RadioButton();
            this.cmb_FreeGoods = new System.Windows.Forms.ComboBox();
            this.lbl_FreeGoods = new System.Windows.Forms.Label();
            this.txt_Find = new MrTextBox();
            this.cmb_Unit = new System.Windows.Forms.ComboBox();
            this.lbl_Unit = new System.Windows.Forms.Label();
            this.lbl_Find = new System.Windows.Forms.Label();
            this.chk_NoBatch = new System.Windows.Forms.CheckBox();
            this.chk_Batch = new System.Windows.Forms.CheckBox();
            this.chk_AltQty = new System.Windows.Forms.CheckBox();
            this.chk_ZeroBalance = new System.Windows.Forms.CheckBox();
            this.chk_Date = new System.Windows.Forms.CheckBox();
            this.Chk_SelectAllGodown = new System.Windows.Forms.CheckBox();
            this.chk_SelectAllProduct = new System.Windows.Forms.CheckBox();
            this.chk_Summary = new System.Windows.Forms.CheckBox();
            this.cmb_GroupBy = new System.Windows.Forms.ComboBox();
            this.lbl_GroupBy = new System.Windows.Forms.Label();
            this.msk_ToDate = new MrMaskedTextBox();
            this.lbl_ToDate = new System.Windows.Forms.Label();
            this.msk_FromDate = new MrMaskedTextBox();
            this.lbl_FromDate = new System.Windows.Forms.Label();
            this.StorePanel = new MrPanel();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Show = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator2 = new ClsSeparator();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSysDateType = new System.Windows.Forms.ComboBox();
            this.clsSeparator1 = new ClsSeparator();
            this.StorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Branch
            // 
            this.lbl_Branch.AutoSize = true;
            this.lbl_Branch.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_Branch.Location = new System.Drawing.Point(4, 159);
            this.lbl_Branch.Name = "lbl_Branch";
            this.lbl_Branch.Size = new System.Drawing.Size(72, 20);
            this.lbl_Branch.TabIndex = 83;
            this.lbl_Branch.Text = "Branch ";
            // 
            // cmb_Branch
            // 
            this.cmb_Branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Branch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_Branch.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.cmb_Branch.FormattingEnabled = true;
            this.cmb_Branch.Location = new System.Drawing.Point(100, 157);
            this.cmb_Branch.Name = "cmb_Branch";
            this.cmb_Branch.Size = new System.Drawing.Size(194, 28);
            this.cmb_Branch.TabIndex = 6;
            // 
            // chk_WithValue
            // 
            this.chk_WithValue.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.chk_WithValue.Location = new System.Drawing.Point(312, 133);
            this.chk_WithValue.Name = "chk_WithValue";
            this.chk_WithValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_WithValue.Size = new System.Drawing.Size(187, 24);
            this.chk_WithValue.TabIndex = 13;
            this.chk_WithValue.Text = "With Value";
            this.chk_WithValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_WithValue.UseVisualStyleBackColor = true;
            // 
            // rb_ProductGodownWise
            // 
            this.rb_ProductGodownWise.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.rb_ProductGodownWise.Location = new System.Drawing.Point(305, 8);
            this.rb_ProductGodownWise.Name = "rb_ProductGodownWise";
            this.rb_ProductGodownWise.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rb_ProductGodownWise.Size = new System.Drawing.Size(164, 24);
            this.rb_ProductGodownWise.TabIndex = 1;
            this.rb_ProductGodownWise.TabStop = true;
            this.rb_ProductGodownWise.Text = "Product/Godown Wise";
            this.rb_ProductGodownWise.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rb_ProductGodownWise.UseVisualStyleBackColor = true;
            // 
            // rb_GodownProductWise
            // 
            this.rb_GodownProductWise.Checked = true;
            this.rb_GodownProductWise.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.rb_GodownProductWise.Location = new System.Drawing.Point(4, 8);
            this.rb_GodownProductWise.Name = "rb_GodownProductWise";
            this.rb_GodownProductWise.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rb_GodownProductWise.Size = new System.Drawing.Size(164, 24);
            this.rb_GodownProductWise.TabIndex = 0;
            this.rb_GodownProductWise.TabStop = true;
            this.rb_GodownProductWise.Text = "Godown/Product Wise";
            this.rb_GodownProductWise.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rb_GodownProductWise.UseVisualStyleBackColor = true;
            // 
            // cmb_FreeGoods
            // 
            this.cmb_FreeGoods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_FreeGoods.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_FreeGoods.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.cmb_FreeGoods.FormattingEnabled = true;
            this.cmb_FreeGoods.Items.AddRange(new object[] {
            "Include Free",
            "Exclude Free",
            "Free Only"});
            this.cmb_FreeGoods.Location = new System.Drawing.Point(100, 224);
            this.cmb_FreeGoods.Name = "cmb_FreeGoods";
            this.cmb_FreeGoods.Size = new System.Drawing.Size(194, 28);
            this.cmb_FreeGoods.TabIndex = 8;
            // 
            // lbl_FreeGoods
            // 
            this.lbl_FreeGoods.AutoSize = true;
            this.lbl_FreeGoods.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_FreeGoods.Location = new System.Drawing.Point(4, 228);
            this.lbl_FreeGoods.Name = "lbl_FreeGoods";
            this.lbl_FreeGoods.Size = new System.Drawing.Size(98, 20);
            this.lbl_FreeGoods.TabIndex = 81;
            this.lbl_FreeGoods.Text = "Free Goods";
            // 
            // txt_Find
            // 
            this.txt_Find.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Find.CausesValidation = false;
            this.txt_Find.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.txt_Find.Location = new System.Drawing.Point(70, 261);
            this.txt_Find.Name = "txt_Find";
            this.txt_Find.Size = new System.Drawing.Size(122, 26);
            this.txt_Find.TabIndex = 13;
            this.txt_Find.Visible = false;
            this.txt_Find.Enter += new System.EventHandler(this.txt_Find_Enter);
            this.txt_Find.Leave += new System.EventHandler(this.txt_Find_Leave);
            // 
            // cmb_Unit
            // 
            this.cmb_Unit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Unit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_Unit.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.cmb_Unit.FormattingEnabled = true;
            this.cmb_Unit.Location = new System.Drawing.Point(100, 190);
            this.cmb_Unit.Name = "cmb_Unit";
            this.cmb_Unit.Size = new System.Drawing.Size(194, 28);
            this.cmb_Unit.TabIndex = 7;
            // 
            // lbl_Unit
            // 
            this.lbl_Unit.AutoSize = true;
            this.lbl_Unit.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_Unit.Location = new System.Drawing.Point(4, 194);
            this.lbl_Unit.Name = "lbl_Unit";
            this.lbl_Unit.Size = new System.Drawing.Size(43, 20);
            this.lbl_Unit.TabIndex = 78;
            this.lbl_Unit.Text = "Unit";
            // 
            // lbl_Find
            // 
            this.lbl_Find.AutoSize = true;
            this.lbl_Find.CausesValidation = false;
            this.lbl_Find.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_Find.Location = new System.Drawing.Point(19, 266);
            this.lbl_Find.Name = "lbl_Find";
            this.lbl_Find.Size = new System.Drawing.Size(45, 20);
            this.lbl_Find.TabIndex = 77;
            this.lbl_Find.Text = "Find";
            this.lbl_Find.Visible = false;
            // 
            // chk_NoBatch
            // 
            this.chk_NoBatch.Enabled = false;
            this.chk_NoBatch.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.chk_NoBatch.Location = new System.Drawing.Point(312, 109);
            this.chk_NoBatch.Name = "chk_NoBatch";
            this.chk_NoBatch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_NoBatch.Size = new System.Drawing.Size(187, 24);
            this.chk_NoBatch.TabIndex = 12;
            this.chk_NoBatch.Text = "No Batch";
            this.chk_NoBatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_NoBatch.UseVisualStyleBackColor = true;
            // 
            // chk_Batch
            // 
            this.chk_Batch.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.chk_Batch.Location = new System.Drawing.Point(312, 85);
            this.chk_Batch.Name = "chk_Batch";
            this.chk_Batch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Batch.Size = new System.Drawing.Size(187, 24);
            this.chk_Batch.TabIndex = 11;
            this.chk_Batch.Text = "Batch";
            this.chk_Batch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Batch.UseVisualStyleBackColor = true;
            // 
            // chk_AltQty
            // 
            this.chk_AltQty.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.chk_AltQty.Location = new System.Drawing.Point(312, 61);
            this.chk_AltQty.Name = "chk_AltQty";
            this.chk_AltQty.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_AltQty.Size = new System.Drawing.Size(187, 24);
            this.chk_AltQty.TabIndex = 10;
            this.chk_AltQty.Text = "Alt Qty";
            this.chk_AltQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_AltQty.UseVisualStyleBackColor = true;
            // 
            // chk_ZeroBalance
            // 
            this.chk_ZeroBalance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.chk_ZeroBalance.Location = new System.Drawing.Point(312, 226);
            this.chk_ZeroBalance.Name = "chk_ZeroBalance";
            this.chk_ZeroBalance.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_ZeroBalance.Size = new System.Drawing.Size(187, 24);
            this.chk_ZeroBalance.TabIndex = 17;
            this.chk_ZeroBalance.Text = "Zero Balance";
            this.chk_ZeroBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_ZeroBalance.UseVisualStyleBackColor = true;
            // 
            // chk_Date
            // 
            this.chk_Date.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.chk_Date.Location = new System.Drawing.Point(312, 157);
            this.chk_Date.Name = "chk_Date";
            this.chk_Date.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Date.Size = new System.Drawing.Size(187, 24);
            this.chk_Date.TabIndex = 14;
            this.chk_Date.Text = "Date";
            this.chk_Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Date.UseVisualStyleBackColor = true;
            // 
            // Chk_SelectAllGodown
            // 
            this.Chk_SelectAllGodown.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.Chk_SelectAllGodown.Location = new System.Drawing.Point(312, 181);
            this.Chk_SelectAllGodown.Name = "Chk_SelectAllGodown";
            this.Chk_SelectAllGodown.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Chk_SelectAllGodown.Size = new System.Drawing.Size(187, 24);
            this.Chk_SelectAllGodown.TabIndex = 15;
            this.Chk_SelectAllGodown.Text = "Select All Godown";
            this.Chk_SelectAllGodown.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Chk_SelectAllGodown.UseVisualStyleBackColor = true;
            // 
            // chk_SelectAllProduct
            // 
            this.chk_SelectAllProduct.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.chk_SelectAllProduct.Location = new System.Drawing.Point(312, 205);
            this.chk_SelectAllProduct.Name = "chk_SelectAllProduct";
            this.chk_SelectAllProduct.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_SelectAllProduct.Size = new System.Drawing.Size(187, 24);
            this.chk_SelectAllProduct.TabIndex = 16;
            this.chk_SelectAllProduct.Text = "Select All Product";
            this.chk_SelectAllProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_SelectAllProduct.UseVisualStyleBackColor = true;
            // 
            // chk_Summary
            // 
            this.chk_Summary.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.chk_Summary.Location = new System.Drawing.Point(312, 37);
            this.chk_Summary.Name = "chk_Summary";
            this.chk_Summary.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Summary.Size = new System.Drawing.Size(187, 24);
            this.chk_Summary.TabIndex = 9;
            this.chk_Summary.Text = "Summary";
            this.chk_Summary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Summary.UseVisualStyleBackColor = true;
            // 
            // cmb_GroupBy
            // 
            this.cmb_GroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_GroupBy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_GroupBy.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.cmb_GroupBy.FormattingEnabled = true;
            this.cmb_GroupBy.Items.AddRange(new object[] {
            "Product",
            "Product Group",
            "Product Sub Group"});
            this.cmb_GroupBy.Location = new System.Drawing.Point(100, 125);
            this.cmb_GroupBy.Name = "cmb_GroupBy";
            this.cmb_GroupBy.Size = new System.Drawing.Size(194, 28);
            this.cmb_GroupBy.TabIndex = 5;
            // 
            // lbl_GroupBy
            // 
            this.lbl_GroupBy.AutoSize = true;
            this.lbl_GroupBy.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_GroupBy.Location = new System.Drawing.Point(4, 129);
            this.lbl_GroupBy.Name = "lbl_GroupBy";
            this.lbl_GroupBy.Size = new System.Drawing.Size(85, 20);
            this.lbl_GroupBy.TabIndex = 64;
            this.lbl_GroupBy.Text = "Group By";
            // 
            // msk_ToDate
            // 
            this.msk_ToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_ToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.msk_ToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.msk_ToDate.Location = new System.Drawing.Point(100, 95);
            this.msk_ToDate.Mask = "00/00/0000";
            this.msk_ToDate.Name = "msk_ToDate";
            this.msk_ToDate.Size = new System.Drawing.Size(122, 26);
            this.msk_ToDate.TabIndex = 4;
            this.msk_ToDate.Enter += new System.EventHandler(this.msk_ToDate_Enter);
            this.msk_ToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msk_ToDate_KeyDown);
            this.msk_ToDate.Leave += new System.EventHandler(this.msk_ToDate_Leave);
            this.msk_ToDate.Validated += new System.EventHandler(this.msk_ToDate_Validated);
            // 
            // lbl_ToDate
            // 
            this.lbl_ToDate.AutoSize = true;
            this.lbl_ToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_ToDate.Location = new System.Drawing.Point(4, 98);
            this.lbl_ToDate.Name = "lbl_ToDate";
            this.lbl_ToDate.Size = new System.Drawing.Size(69, 20);
            this.lbl_ToDate.TabIndex = 57;
            this.lbl_ToDate.Text = "To Date";
            // 
            // msk_FromDate
            // 
            this.msk_FromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_FromDate.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.msk_FromDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.msk_FromDate.Location = new System.Drawing.Point(100, 67);
            this.msk_FromDate.Mask = "00/00/0000";
            this.msk_FromDate.Name = "msk_FromDate";
            this.msk_FromDate.Size = new System.Drawing.Size(122, 26);
            this.msk_FromDate.TabIndex = 3;
            this.msk_FromDate.Enter += new System.EventHandler(this.msk_FromDate_Enter);
            this.msk_FromDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msk_FromDate_KeyDown);
            this.msk_FromDate.Leave += new System.EventHandler(this.msk_FromDate_Leave);
            this.msk_FromDate.Validated += new System.EventHandler(this.msk_FromDate_Validated);
            // 
            // lbl_FromDate
            // 
            this.lbl_FromDate.AutoSize = true;
            this.lbl_FromDate.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_FromDate.Location = new System.Drawing.Point(4, 70);
            this.lbl_FromDate.Name = "lbl_FromDate";
            this.lbl_FromDate.Size = new System.Drawing.Size(92, 20);
            this.lbl_FromDate.TabIndex = 55;
            this.lbl_FromDate.Text = "From Date";
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.simpleButton2);
            this.StorePanel.Controls.Add(this.btn_Show);
            this.StorePanel.Controls.Add(this.clsSeparator2);
            this.StorePanel.Controls.Add(this.chk_WithValue);
            this.StorePanel.Controls.Add(this.chk_ZeroBalance);
            this.StorePanel.Controls.Add(this.label4);
            this.StorePanel.Controls.Add(this.chk_Date);
            this.StorePanel.Controls.Add(this.txt_Find);
            this.StorePanel.Controls.Add(this.Chk_SelectAllGodown);
            this.StorePanel.Controls.Add(this.lbl_Find);
            this.StorePanel.Controls.Add(this.chk_SelectAllProduct);
            this.StorePanel.Controls.Add(this.cmb_FreeGoods);
            this.StorePanel.Controls.Add(this.cmbSysDateType);
            this.StorePanel.Controls.Add(this.lbl_FreeGoods);
            this.StorePanel.Controls.Add(this.lbl_Branch);
            this.StorePanel.Controls.Add(this.cmb_Unit);
            this.StorePanel.Controls.Add(this.lbl_Unit);
            this.StorePanel.Controls.Add(this.clsSeparator1);
            this.StorePanel.Controls.Add(this.cmb_Branch);
            this.StorePanel.Controls.Add(this.chk_NoBatch);
            this.StorePanel.Controls.Add(this.chk_Batch);
            this.StorePanel.Controls.Add(this.rb_ProductGodownWise);
            this.StorePanel.Controls.Add(this.chk_AltQty);
            this.StorePanel.Controls.Add(this.rb_GodownProductWise);
            this.StorePanel.Controls.Add(this.msk_FromDate);
            this.StorePanel.Controls.Add(this.lbl_FromDate);
            this.StorePanel.Controls.Add(this.lbl_ToDate);
            this.StorePanel.Controls.Add(this.chk_Summary);
            this.StorePanel.Controls.Add(this.msk_ToDate);
            this.StorePanel.Controls.Add(this.lbl_GroupBy);
            this.StorePanel.Controls.Add(this.cmb_GroupBy);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(507, 303);
            this.StorePanel.TabIndex = 0;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(332, 259);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(112, 39);
            this.simpleButton2.TabIndex = 19;
            this.simpleButton2.Text = "&CANCEL";
            // 
            // btn_Show
            // 
            this.btn_Show.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Show.Appearance.Options.UseFont = true;
            this.btn_Show.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.btn_Show.Location = new System.Drawing.Point(225, 259);
            this.btn_Show.Name = "btn_Show";
            this.btn_Show.Size = new System.Drawing.Size(107, 39);
            this.btn_Show.TabIndex = 18;
            this.btn_Show.Text = "&SHOW";
            this.btn_Show.Click += new System.EventHandler(this.btn_Show_Click);
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator2.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.clsSeparator2.Location = new System.Drawing.Point(9, 253);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(490, 2);
            this.clsSeparator2.TabIndex = 12;
            this.clsSeparator2.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label4.Location = new System.Drawing.Point(4, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 20);
            this.label4.TabIndex = 84;
            this.label4.Text = "Date Type";
            // 
            // cmbSysDateType
            // 
            this.cmbSysDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSysDateType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbSysDateType.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.cmbSysDateType.FormattingEnabled = true;
            this.cmbSysDateType.Items.AddRange(new object[] {
            "Custom Date",
            "Today",
            "Yesterday",
            "Current Week",
            "Last Week",
            "Current Month",
            "Last Month",
            "Upto Date",
            "Accounting Period"});
            this.cmbSysDateType.Location = new System.Drawing.Point(100, 37);
            this.cmbSysDateType.Name = "cmbSysDateType";
            this.cmbSysDateType.Size = new System.Drawing.Size(194, 28);
            this.cmbSysDateType.TabIndex = 2;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator1.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.clsSeparator1.Location = new System.Drawing.Point(9, 32);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(490, 2);
            this.clsSeparator1.TabIndex = 11;
            this.clsSeparator1.TabStop = false;
            // 
            // FrmGodownProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(507, 303);
            this.Controls.Add(this.StorePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmGodownProduct";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Ledger Godown/Product Wise Report";
            this.Load += new System.EventHandler(this.FrmGodownProduct_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmGodownProduct_KeyPress);
            this.StorePanel.ResumeLayout(false);
            this.StorePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox chk_NoBatch;
        private System.Windows.Forms.CheckBox chk_Batch;
        private System.Windows.Forms.CheckBox chk_AltQty;
        private System.Windows.Forms.CheckBox chk_ZeroBalance;
        private System.Windows.Forms.CheckBox chk_Date;
        private System.Windows.Forms.CheckBox Chk_SelectAllGodown;
        private System.Windows.Forms.CheckBox chk_SelectAllProduct;
        private System.Windows.Forms.CheckBox chk_Summary;
        private System.Windows.Forms.ComboBox cmb_GroupBy;
        private System.Windows.Forms.Label lbl_GroupBy;
        private System.Windows.Forms.Label lbl_ToDate;
        private System.Windows.Forms.Label lbl_FromDate;
        private System.Windows.Forms.ComboBox cmb_Unit;
        private System.Windows.Forms.Label lbl_Unit;
        private System.Windows.Forms.Label lbl_Find;
        private System.Windows.Forms.ComboBox cmb_FreeGoods;
        private System.Windows.Forms.Label lbl_FreeGoods;
        private System.Windows.Forms.RadioButton rb_ProductGodownWise;
        private System.Windows.Forms.RadioButton rb_GodownProductWise;
        private System.Windows.Forms.CheckBox chk_WithValue;
        private System.Windows.Forms.Label lbl_Branch;
        private System.Windows.Forms.ComboBox cmb_Branch;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSysDateType;
        private ClsSeparator clsSeparator2;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton btn_Show;
        private MrMaskedTextBox msk_ToDate;
        private MrMaskedTextBox msk_FromDate;
        private MrTextBox txt_Find;
        private MrPanel StorePanel;
    }
}