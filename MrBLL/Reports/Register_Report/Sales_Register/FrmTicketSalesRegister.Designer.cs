using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.Sales_Register
{
    partial class FrmTicketSalesRegister
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
            this.lbl_AdvanceReport = new System.Windows.Forms.Label();
            this.cmb_AdvanceReport = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSysDateType = new System.Windows.Forms.ComboBox();
            this.cmb_InvoiceType = new System.Windows.Forms.ComboBox();
            this.lbl_InvoiceType = new System.Windows.Forms.Label();
            this.chk_Horizontal = new System.Windows.Forms.CheckBox();
            this.chk_Class = new System.Windows.Forms.CheckBox();
            this.btn_Show = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Class = new System.Windows.Forms.Button();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.gb_TBOptions = new System.Windows.Forms.GroupBox();
            this.chk_ViewDxReport = new System.Windows.Forms.CheckBox();
            this.txt_Class = new MrTextBox();
            this.lbl_Branch = new System.Windows.Forms.Label();
            this.cmb_Branch = new System.Windows.Forms.ComboBox();
            this.txt_Find = new MrTextBox();
            this.lbl_Find = new System.Windows.Forms.Label();
            this.chk_Date = new System.Windows.Forms.CheckBox();
            this.chk_SelectAll = new System.Windows.Forms.CheckBox();
            this.chk_Remarks = new System.Windows.Forms.CheckBox();
            this.cmb_GroupBy = new System.Windows.Forms.ComboBox();
            this.lbl_GroupBy = new System.Windows.Forms.Label();
            this.cmb_Currency = new System.Windows.Forms.ComboBox();
            this.lbl_Currency = new System.Windows.Forms.Label();
            this.chk_Summary = new System.Windows.Forms.CheckBox();
            this.msk_ToDate = new MrMaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.msk_FromDate = new MrMaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gb_TBOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_AdvanceReport
            // 
            this.lbl_AdvanceReport.AutoSize = true;
            this.lbl_AdvanceReport.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AdvanceReport.Location = new System.Drawing.Point(518, 122);
            this.lbl_AdvanceReport.Name = "lbl_AdvanceReport";
            this.lbl_AdvanceReport.Size = new System.Drawing.Size(132, 20);
            this.lbl_AdvanceReport.TabIndex = 192;
            this.lbl_AdvanceReport.Text = "Advance Report";
            // 
            // cmb_AdvanceReport
            // 
            this.cmb_AdvanceReport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_AdvanceReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_AdvanceReport.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_AdvanceReport.FormattingEnabled = true;
            this.cmb_AdvanceReport.Location = new System.Drawing.Point(514, 145);
            this.cmb_AdvanceReport.Name = "cmb_AdvanceReport";
            this.cmb_AdvanceReport.Size = new System.Drawing.Size(170, 26);
            this.cmb_AdvanceReport.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 20);
            this.label4.TabIndex = 190;
            this.label4.Text = "Date Type";
            // 
            // cmbSysDateType
            // 
            this.cmbSysDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSysDateType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSysDateType.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.cmbSysDateType.Location = new System.Drawing.Point(144, 13);
            this.cmbSysDateType.Name = "cmbSysDateType";
            this.cmbSysDateType.Size = new System.Drawing.Size(180, 26);
            this.cmbSysDateType.TabIndex = 0;
            this.cmbSysDateType.SelectedIndexChanged += new System.EventHandler(this.cmbSysDateType_SelectedIndexChanged);
            // 
            // cmb_InvoiceType
            // 
            this.cmb_InvoiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_InvoiceType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_InvoiceType.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_InvoiceType.FormattingEnabled = true;
            this.cmb_InvoiceType.Items.AddRange(new object[] {
            "Both",
            "Cash",
            "Credit"});
            this.cmb_InvoiceType.Location = new System.Drawing.Point(144, 129);
            this.cmb_InvoiceType.Name = "cmb_InvoiceType";
            this.cmb_InvoiceType.Size = new System.Drawing.Size(180, 26);
            this.cmb_InvoiceType.TabIndex = 4;
            // 
            // lbl_InvoiceType
            // 
            this.lbl_InvoiceType.AutoSize = true;
            this.lbl_InvoiceType.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_InvoiceType.Location = new System.Drawing.Point(6, 133);
            this.lbl_InvoiceType.Name = "lbl_InvoiceType";
            this.lbl_InvoiceType.Size = new System.Drawing.Size(105, 20);
            this.lbl_InvoiceType.TabIndex = 169;
            this.lbl_InvoiceType.Text = "Invoice Type";
            // 
            // chk_Horizontal
            // 
            this.chk_Horizontal.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Horizontal.Location = new System.Drawing.Point(330, 52);
            this.chk_Horizontal.Name = "chk_Horizontal";
            this.chk_Horizontal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Horizontal.Size = new System.Drawing.Size(171, 27);
            this.chk_Horizontal.TabIndex = 11;
            this.chk_Horizontal.Text = "Horizontal";
            this.chk_Horizontal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Horizontal.UseVisualStyleBackColor = true;
            // 
            // chk_Class
            // 
            this.chk_Class.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Class.Location = new System.Drawing.Point(511, 17);
            this.chk_Class.Name = "chk_Class";
            this.chk_Class.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Class.Size = new System.Drawing.Size(173, 24);
            this.chk_Class.TabIndex = 20;
            this.chk_Class.Text = "Document Class";
            this.chk_Class.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Class.UseVisualStyleBackColor = true;
            this.chk_Class.Click += new System.EventHandler(this.chk_Class_Click);
            // 
            // btn_Show
            // 
            this.btn_Show.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Show.Appearance.Options.UseFont = true;
            this.btn_Show.Location = new System.Drawing.Point(499, 227);
            this.btn_Show.Name = "btn_Show";
            this.btn_Show.Size = new System.Drawing.Size(89, 37);
            this.btn_Show.TabIndex = 28;
            this.btn_Show.Text = "&SHOW";
            this.btn_Show.Click += new System.EventHandler(this.btn_Show_Click);
            // 
            // btn_Class
            // 
            this.btn_Class.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Class.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Class.ForeColor = System.Drawing.SystemColors.Window;
            this.btn_Class.Location = new System.Drawing.Point(655, 41);
            this.btn_Class.Name = "btn_Class";
            this.btn_Class.Size = new System.Drawing.Size(31, 25);
            this.btn_Class.TabIndex = 21;
            this.btn_Class.TabStop = false;
            this.btn_Class.Text = "**";
            this.btn_Class.UseVisualStyleBackColor = true;
            this.btn_Class.Click += new System.EventHandler(this.btn_Class_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Appearance.Options.UseFont = true;
            this.btn_Cancel.Location = new System.Drawing.Point(590, 227);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(100, 37);
            this.btn_Cancel.TabIndex = 29;
            this.btn_Cancel.Text = "&CANCEL";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // gb_TBOptions
            // 
            this.gb_TBOptions.Controls.Add(this.chk_ViewDxReport);
            this.gb_TBOptions.Controls.Add(this.lbl_AdvanceReport);
            this.gb_TBOptions.Controls.Add(this.cmb_AdvanceReport);
            this.gb_TBOptions.Controls.Add(this.label4);
            this.gb_TBOptions.Controls.Add(this.cmbSysDateType);
            this.gb_TBOptions.Controls.Add(this.cmb_InvoiceType);
            this.gb_TBOptions.Controls.Add(this.lbl_InvoiceType);
            this.gb_TBOptions.Controls.Add(this.chk_Horizontal);
            this.gb_TBOptions.Controls.Add(this.chk_Class);
            this.gb_TBOptions.Controls.Add(this.btn_Class);
            this.gb_TBOptions.Controls.Add(this.txt_Class);
            this.gb_TBOptions.Controls.Add(this.lbl_Branch);
            this.gb_TBOptions.Controls.Add(this.cmb_Branch);
            this.gb_TBOptions.Controls.Add(this.txt_Find);
            this.gb_TBOptions.Controls.Add(this.lbl_Find);
            this.gb_TBOptions.Controls.Add(this.chk_Date);
            this.gb_TBOptions.Controls.Add(this.chk_SelectAll);
            this.gb_TBOptions.Controls.Add(this.chk_Remarks);
            this.gb_TBOptions.Controls.Add(this.cmb_GroupBy);
            this.gb_TBOptions.Controls.Add(this.lbl_GroupBy);
            this.gb_TBOptions.Controls.Add(this.cmb_Currency);
            this.gb_TBOptions.Controls.Add(this.lbl_Currency);
            this.gb_TBOptions.Controls.Add(this.chk_Summary);
            this.gb_TBOptions.Controls.Add(this.msk_ToDate);
            this.gb_TBOptions.Controls.Add(this.label2);
            this.gb_TBOptions.Controls.Add(this.msk_FromDate);
            this.gb_TBOptions.Controls.Add(this.label1);
            this.gb_TBOptions.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_TBOptions.Location = new System.Drawing.Point(4, -3);
            this.gb_TBOptions.Name = "gb_TBOptions";
            this.gb_TBOptions.Size = new System.Drawing.Size(691, 226);
            this.gb_TBOptions.TabIndex = 27;
            this.gb_TBOptions.TabStop = false;
            // 
            // chk_ViewDxReport
            // 
            this.chk_ViewDxReport.AllowDrop = true;
            this.chk_ViewDxReport.Cursor = System.Windows.Forms.Cursors.Default;
            this.chk_ViewDxReport.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_ViewDxReport.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chk_ViewDxReport.Location = new System.Drawing.Point(330, 157);
            this.chk_ViewDxReport.Name = "chk_ViewDxReport";
            this.chk_ViewDxReport.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_ViewDxReport.Size = new System.Drawing.Size(171, 32);
            this.chk_ViewDxReport.TabIndex = 193;
            this.chk_ViewDxReport.Text = "Dynamic Report";
            this.chk_ViewDxReport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_ViewDxReport.UseMnemonic = false;
            this.chk_ViewDxReport.UseVisualStyleBackColor = true;
            // 
            // txt_Class
            // 
            this.txt_Class.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txt_Class.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Class.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Class.Location = new System.Drawing.Point(514, 41);
            this.txt_Class.MaxLength = 255;
            this.txt_Class.Name = "txt_Class";
            this.txt_Class.Size = new System.Drawing.Size(171, 26);
            this.txt_Class.TabIndex = 21;
            this.txt_Class.Enter += new System.EventHandler(this.txt_Class_Enter);
            this.txt_Class.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Class_KeyDown);
            this.txt_Class.Leave += new System.EventHandler(this.txt_Class_Leave);
            // 
            // lbl_Branch
            // 
            this.lbl_Branch.AutoSize = true;
            this.lbl_Branch.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Branch.Location = new System.Drawing.Point(6, 163);
            this.lbl_Branch.Name = "lbl_Branch";
            this.lbl_Branch.Size = new System.Drawing.Size(72, 20);
            this.lbl_Branch.TabIndex = 75;
            this.lbl_Branch.Text = "Branch ";
            // 
            // cmb_Branch
            // 
            this.cmb_Branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Branch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_Branch.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Branch.FormattingEnabled = true;
            this.cmb_Branch.Location = new System.Drawing.Point(144, 161);
            this.cmb_Branch.Name = "cmb_Branch";
            this.cmb_Branch.Size = new System.Drawing.Size(180, 26);
            this.cmb_Branch.TabIndex = 6;
            // 
            // txt_Find
            // 
            this.txt_Find.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Find.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Find.Location = new System.Drawing.Point(514, 91);
            this.txt_Find.Name = "txt_Find";
            this.txt_Find.Size = new System.Drawing.Size(167, 26);
            this.txt_Find.TabIndex = 24;
            this.txt_Find.Enter += new System.EventHandler(this.txt_Find_Enter);
            this.txt_Find.Leave += new System.EventHandler(this.txt_Find_Leave);
            // 
            // lbl_Find
            // 
            this.lbl_Find.AutoSize = true;
            this.lbl_Find.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Find.Location = new System.Drawing.Point(515, 71);
            this.lbl_Find.Name = "lbl_Find";
            this.lbl_Find.Size = new System.Drawing.Size(104, 20);
            this.lbl_Find.TabIndex = 23;
            this.lbl_Find.Text = "Find Invoice";
            // 
            // chk_Date
            // 
            this.chk_Date.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Date.Location = new System.Drawing.Point(330, 122);
            this.chk_Date.Name = "chk_Date";
            this.chk_Date.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Date.Size = new System.Drawing.Size(171, 27);
            this.chk_Date.TabIndex = 19;
            this.chk_Date.Text = "Date";
            this.chk_Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Date.UseVisualStyleBackColor = true;
            // 
            // chk_SelectAll
            // 
            this.chk_SelectAll.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_SelectAll.Location = new System.Drawing.Point(330, 197);
            this.chk_SelectAll.Name = "chk_SelectAll";
            this.chk_SelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_SelectAll.Size = new System.Drawing.Size(171, 20);
            this.chk_SelectAll.TabIndex = 22;
            this.chk_SelectAll.Text = "Select All";
            this.chk_SelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_SelectAll.UseVisualStyleBackColor = true;
            // 
            // chk_Remarks
            // 
            this.chk_Remarks.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Remarks.Location = new System.Drawing.Point(330, 87);
            this.chk_Remarks.Name = "chk_Remarks";
            this.chk_Remarks.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Remarks.Size = new System.Drawing.Size(171, 27);
            this.chk_Remarks.TabIndex = 16;
            this.chk_Remarks.Text = "Remarks";
            this.chk_Remarks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Remarks.UseVisualStyleBackColor = true;
            // 
            // cmb_GroupBy
            // 
            this.cmb_GroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_GroupBy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_GroupBy.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_GroupBy.FormattingEnabled = true;
            this.cmb_GroupBy.Items.AddRange(new object[] {
            "Date",
            "Invoice",
            "Party",
            "Agent",
            "Area",
            "Product",
            "Product Group",
            "Product Sub Group"});
            this.cmb_GroupBy.Location = new System.Drawing.Point(144, 99);
            this.cmb_GroupBy.Name = "cmb_GroupBy";
            this.cmb_GroupBy.Size = new System.Drawing.Size(180, 28);
            this.cmb_GroupBy.TabIndex = 3;
            this.cmb_GroupBy.SelectedIndexChanged += new System.EventHandler(this.cmb_GroupBy_SelectedIndexChanged);
            // 
            // lbl_GroupBy
            // 
            this.lbl_GroupBy.AutoSize = true;
            this.lbl_GroupBy.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_GroupBy.Location = new System.Drawing.Point(6, 104);
            this.lbl_GroupBy.Name = "lbl_GroupBy";
            this.lbl_GroupBy.Size = new System.Drawing.Size(85, 20);
            this.lbl_GroupBy.TabIndex = 64;
            this.lbl_GroupBy.Text = "Group By";
            // 
            // cmb_Currency
            // 
            this.cmb_Currency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Currency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_Currency.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Currency.FormattingEnabled = true;
            this.cmb_Currency.Items.AddRange(new object[] {
            "Local",
            "Foreign",
            "Both"});
            this.cmb_Currency.Location = new System.Drawing.Point(144, 192);
            this.cmb_Currency.Name = "cmb_Currency";
            this.cmb_Currency.Size = new System.Drawing.Size(180, 26);
            this.cmb_Currency.TabIndex = 7;
            // 
            // lbl_Currency
            // 
            this.lbl_Currency.AutoSize = true;
            this.lbl_Currency.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Currency.Location = new System.Drawing.Point(6, 194);
            this.lbl_Currency.Name = "lbl_Currency";
            this.lbl_Currency.Size = new System.Drawing.Size(83, 20);
            this.lbl_Currency.TabIndex = 62;
            this.lbl_Currency.Text = "Currency";
            // 
            // chk_Summary
            // 
            this.chk_Summary.Checked = true;
            this.chk_Summary.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Summary.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Summary.Location = new System.Drawing.Point(330, 15);
            this.chk_Summary.Name = "chk_Summary";
            this.chk_Summary.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Summary.Size = new System.Drawing.Size(171, 29);
            this.chk_Summary.TabIndex = 9;
            this.chk_Summary.Text = "Summary";
            this.chk_Summary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Summary.UseVisualStyleBackColor = true;
            this.chk_Summary.CheckedChanged += new System.EventHandler(this.chk_Summary_CheckedChanged);
            // 
            // msk_ToDate
            // 
            this.msk_ToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_ToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msk_ToDate.Location = new System.Drawing.Point(144, 71);
            this.msk_ToDate.Mask = "00/00/0000";
            this.msk_ToDate.Name = "msk_ToDate";
            this.msk_ToDate.Size = new System.Drawing.Size(129, 26);
            this.msk_ToDate.TabIndex = 2;
            this.msk_ToDate.Enter += new System.EventHandler(this.msk_ToDate_Enter);
            this.msk_ToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msk_ToDate_KeyDown);
            this.msk_ToDate.Leave += new System.EventHandler(this.msk_ToDate_Leave);
            this.msk_ToDate.Validated += new System.EventHandler(this.msk_ToDate_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 57;
            this.label2.Text = "To Date";
            // 
            // msk_FromDate
            // 
            this.msk_FromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_FromDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msk_FromDate.Location = new System.Drawing.Point(144, 43);
            this.msk_FromDate.Mask = "00/00/0000";
            this.msk_FromDate.Name = "msk_FromDate";
            this.msk_FromDate.Size = new System.Drawing.Size(129, 26);
            this.msk_FromDate.TabIndex = 1;
            this.msk_FromDate.Enter += new System.EventHandler(this.msk_FromDate_Enter);
            this.msk_FromDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msk_FromDate_KeyDown);
            this.msk_FromDate.Leave += new System.EventHandler(this.msk_FromDate_Leave);
            this.msk_FromDate.Validated += new System.EventHandler(this.msk_FromDate_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 20);
            this.label1.TabIndex = 55;
            this.label1.Text = "From Date";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(4, 217);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(691, 52);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            // 
            // FrmTicketSalesRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(698, 268);
            this.Controls.Add(this.btn_Show);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.gb_TBOptions);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmTicketSalesRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ticket Sales Register";
            this.Activated += new System.EventHandler(this.FrmTicketSalesRegister_Activated);
            this.Load += new System.EventHandler(this.FrmTicketSalesRegister_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmTicketSalesRegister_KeyPress);
            this.gb_TBOptions.ResumeLayout(false);
            this.gb_TBOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_AdvanceReport;
        private System.Windows.Forms.ComboBox cmb_AdvanceReport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSysDateType;
        private System.Windows.Forms.ComboBox cmb_InvoiceType;
        private System.Windows.Forms.Label lbl_InvoiceType;
        private System.Windows.Forms.CheckBox chk_Horizontal;
        private System.Windows.Forms.CheckBox chk_Class;
        private DevExpress.XtraEditors.SimpleButton btn_Show;
        private System.Windows.Forms.Button btn_Class;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private System.Windows.Forms.GroupBox gb_TBOptions;
        private System.Windows.Forms.TextBox txt_Class;
        private System.Windows.Forms.Label lbl_Branch;
        private System.Windows.Forms.ComboBox cmb_Branch;
        private System.Windows.Forms.TextBox txt_Find;
        private System.Windows.Forms.Label lbl_Find;
        private System.Windows.Forms.CheckBox chk_Date;
        private System.Windows.Forms.CheckBox chk_SelectAll;
        private System.Windows.Forms.CheckBox chk_Remarks;
        private System.Windows.Forms.ComboBox cmb_GroupBy;
        private System.Windows.Forms.Label lbl_GroupBy;
        private System.Windows.Forms.ComboBox cmb_Currency;
        private System.Windows.Forms.Label lbl_Currency;
        private System.Windows.Forms.CheckBox chk_Summary;
        private System.Windows.Forms.MaskedTextBox msk_ToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox msk_FromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chk_ViewDxReport;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}