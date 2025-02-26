using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.Register_Report
{
    partial class FrmCreditNoteRegister
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
            this.chk_IncludeSubedger = new System.Windows.Forms.CheckBox();
            this.gb_TBOptions = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSysDateType = new System.Windows.Forms.ComboBox();
            this.cmb_FilterFor = new System.Windows.Forms.ComboBox();
            this.lbl_FilterFor = new System.Windows.Forms.Label();
            this.cmb_InvoiceType = new System.Windows.Forms.ComboBox();
            this.lbl_InvoiceType = new System.Windows.Forms.Label();
            this.btn_Class = new System.Windows.Forms.Button();
            this.txt_Class = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_Branch = new System.Windows.Forms.Label();
            this.cmb_Branch = new System.Windows.Forms.ComboBox();
            this.txt_Find = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_Find = new System.Windows.Forms.Label();
            this.chk_Date = new System.Windows.Forms.CheckBox();
            this.chk_SelectAll = new System.Windows.Forms.CheckBox();
            this.chk_Remarks = new System.Windows.Forms.CheckBox();
            this.cmb_GroupBy = new System.Windows.Forms.ComboBox();
            this.lbl_GroupBy = new System.Windows.Forms.Label();
            this.cmb_Currency = new System.Windows.Forms.ComboBox();
            this.lbl_Currency = new System.Windows.Forms.Label();
            this.chk_Summary = new System.Windows.Forms.CheckBox();
            this.msk_ToDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.msk_FromDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chk_Horizontal = new System.Windows.Forms.CheckBox();
            this.chk_Class = new System.Windows.Forms.CheckBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gb_TBOptions.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chk_IncludeSubedger
            // 
            this.chk_IncludeSubedger.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_IncludeSubedger.Location = new System.Drawing.Point(341, 108);
            this.chk_IncludeSubedger.Name = "chk_IncludeSubedger";
            this.chk_IncludeSubedger.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_IncludeSubedger.Size = new System.Drawing.Size(180, 23);
            this.chk_IncludeSubedger.TabIndex = 191;
            this.chk_IncludeSubedger.Text = "SubLedger";
            this.chk_IncludeSubedger.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_IncludeSubedger.UseVisualStyleBackColor = true;
            // 
            // gb_TBOptions
            // 
            this.gb_TBOptions.Controls.Add(this.chk_IncludeSubedger);
            this.gb_TBOptions.Controls.Add(this.label4);
            this.gb_TBOptions.Controls.Add(this.cmbSysDateType);
            this.gb_TBOptions.Controls.Add(this.cmb_FilterFor);
            this.gb_TBOptions.Controls.Add(this.lbl_FilterFor);
            this.gb_TBOptions.Controls.Add(this.cmb_InvoiceType);
            this.gb_TBOptions.Controls.Add(this.lbl_InvoiceType);
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
            this.gb_TBOptions.Controls.Add(this.chk_Horizontal);
            this.gb_TBOptions.Controls.Add(this.chk_Class);
            this.gb_TBOptions.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_TBOptions.Location = new System.Drawing.Point(2, -7);
            this.gb_TBOptions.Name = "gb_TBOptions";
            this.gb_TBOptions.Size = new System.Drawing.Size(535, 252);
            this.gb_TBOptions.TabIndex = 37;
            this.gb_TBOptions.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 20);
            this.label4.TabIndex = 190;
            this.label4.Text = "Date Type";
            // 
            // cmbSysDateType
            // 
            this.cmbSysDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSysDateType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSysDateType.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.cmbSysDateType.Location = new System.Drawing.Point(111, 13);
            this.cmbSysDateType.MaxLength = 50;
            this.cmbSysDateType.Name = "cmbSysDateType";
            this.cmbSysDateType.Size = new System.Drawing.Size(207, 28);
            this.cmbSysDateType.TabIndex = 0;
            // 
            // cmb_FilterFor
            // 
            this.cmb_FilterFor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_FilterFor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_FilterFor.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_FilterFor.FormattingEnabled = true;
            this.cmb_FilterFor.Location = new System.Drawing.Point(111, 157);
            this.cmb_FilterFor.Name = "cmb_FilterFor";
            this.cmb_FilterFor.Size = new System.Drawing.Size(207, 28);
            this.cmb_FilterFor.TabIndex = 5;
            // 
            // lbl_FilterFor
            // 
            this.lbl_FilterFor.AutoSize = true;
            this.lbl_FilterFor.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_FilterFor.Location = new System.Drawing.Point(4, 159);
            this.lbl_FilterFor.Name = "lbl_FilterFor";
            this.lbl_FilterFor.Size = new System.Drawing.Size(86, 20);
            this.lbl_FilterFor.TabIndex = 171;
            this.lbl_FilterFor.Text = "Report As";
            // 
            // cmb_InvoiceType
            // 
            this.cmb_InvoiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_InvoiceType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_InvoiceType.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_InvoiceType.FormattingEnabled = true;
            this.cmb_InvoiceType.Items.AddRange(new object[] {
            "Both",
            "Cash",
            "Credit"});
            this.cmb_InvoiceType.Location = new System.Drawing.Point(111, 127);
            this.cmb_InvoiceType.Name = "cmb_InvoiceType";
            this.cmb_InvoiceType.Size = new System.Drawing.Size(207, 28);
            this.cmb_InvoiceType.TabIndex = 4;
            // 
            // lbl_InvoiceType
            // 
            this.lbl_InvoiceType.AutoSize = true;
            this.lbl_InvoiceType.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_InvoiceType.Location = new System.Drawing.Point(4, 130);
            this.lbl_InvoiceType.Name = "lbl_InvoiceType";
            this.lbl_InvoiceType.Size = new System.Drawing.Size(105, 20);
            this.lbl_InvoiceType.TabIndex = 169;
            this.lbl_InvoiceType.Text = "Invoice Type";
            // 
            // btn_Class
            // 
            this.btn_Class.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Class.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Class.ForeColor = System.Drawing.SystemColors.Window;
            this.btn_Class.Location = new System.Drawing.Point(494, 82);
            this.btn_Class.Name = "btn_Class";
            this.btn_Class.Size = new System.Drawing.Size(32, 25);
            this.btn_Class.TabIndex = 21;
            this.btn_Class.TabStop = false;
            this.btn_Class.Text = "**";
            this.btn_Class.UseVisualStyleBackColor = true;
            // 
            // txt_Class
            // 
            this.txt_Class.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txt_Class.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Class.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Class.Location = new System.Drawing.Point(341, 82);
            this.txt_Class.MaxLength = 255;
            this.txt_Class.Name = "txt_Class";
            this.txt_Class.Size = new System.Drawing.Size(180, 26);
            this.txt_Class.TabIndex = 11;
            // 
            // lbl_Branch
            // 
            this.lbl_Branch.AutoSize = true;
            this.lbl_Branch.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Branch.Location = new System.Drawing.Point(4, 191);
            this.lbl_Branch.Name = "lbl_Branch";
            this.lbl_Branch.Size = new System.Drawing.Size(72, 20);
            this.lbl_Branch.TabIndex = 75;
            this.lbl_Branch.Text = "Branch ";
            // 
            // cmb_Branch
            // 
            this.cmb_Branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Branch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_Branch.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Branch.FormattingEnabled = true;
            this.cmb_Branch.Location = new System.Drawing.Point(111, 187);
            this.cmb_Branch.Name = "cmb_Branch";
            this.cmb_Branch.Size = new System.Drawing.Size(207, 28);
            this.cmb_Branch.TabIndex = 6;
            // 
            // txt_Find
            // 
            this.txt_Find.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Find.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Find.Location = new System.Drawing.Point(341, 197);
            this.txt_Find.Name = "txt_Find";
            this.txt_Find.Size = new System.Drawing.Size(180, 26);
            this.txt_Find.TabIndex = 14;
            // 
            // lbl_Find
            // 
            this.lbl_Find.AutoSize = true;
            this.lbl_Find.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Find.Location = new System.Drawing.Point(341, 177);
            this.lbl_Find.Name = "lbl_Find";
            this.lbl_Find.Size = new System.Drawing.Size(148, 20);
            this.lbl_Find.TabIndex = 71;
            this.lbl_Find.Text = "Filter Voucher No";
            // 
            // chk_Date
            // 
            this.chk_Date.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Date.Location = new System.Drawing.Point(341, 154);
            this.chk_Date.Name = "chk_Date";
            this.chk_Date.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Date.Size = new System.Drawing.Size(180, 23);
            this.chk_Date.TabIndex = 13;
            this.chk_Date.Text = "Date";
            this.chk_Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Date.UseVisualStyleBackColor = true;
            // 
            // chk_SelectAll
            // 
            this.chk_SelectAll.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_SelectAll.Location = new System.Drawing.Point(341, 223);
            this.chk_SelectAll.Name = "chk_SelectAll";
            this.chk_SelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_SelectAll.Size = new System.Drawing.Size(180, 23);
            this.chk_SelectAll.TabIndex = 15;
            this.chk_SelectAll.Text = "Select All";
            this.chk_SelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_SelectAll.UseVisualStyleBackColor = true;
            // 
            // chk_Remarks
            // 
            this.chk_Remarks.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Remarks.Location = new System.Drawing.Point(341, 131);
            this.chk_Remarks.Name = "chk_Remarks";
            this.chk_Remarks.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Remarks.Size = new System.Drawing.Size(180, 23);
            this.chk_Remarks.TabIndex = 12;
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
            this.cmb_GroupBy.Location = new System.Drawing.Point(111, 97);
            this.cmb_GroupBy.Name = "cmb_GroupBy";
            this.cmb_GroupBy.Size = new System.Drawing.Size(207, 28);
            this.cmb_GroupBy.TabIndex = 3;
            // 
            // lbl_GroupBy
            // 
            this.lbl_GroupBy.AutoSize = true;
            this.lbl_GroupBy.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_GroupBy.Location = new System.Drawing.Point(4, 98);
            this.lbl_GroupBy.Name = "lbl_GroupBy";
            this.lbl_GroupBy.Size = new System.Drawing.Size(85, 20);
            this.lbl_GroupBy.TabIndex = 64;
            this.lbl_GroupBy.Text = "Group By";
            // 
            // cmb_Currency
            // 
            this.cmb_Currency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Currency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_Currency.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Currency.FormattingEnabled = true;
            this.cmb_Currency.Items.AddRange(new object[] {
            "Local",
            "Foreign",
            "Both"});
            this.cmb_Currency.Location = new System.Drawing.Point(111, 217);
            this.cmb_Currency.Name = "cmb_Currency";
            this.cmb_Currency.Size = new System.Drawing.Size(207, 28);
            this.cmb_Currency.TabIndex = 7;
            // 
            // lbl_Currency
            // 
            this.lbl_Currency.AutoSize = true;
            this.lbl_Currency.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Currency.Location = new System.Drawing.Point(4, 221);
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
            this.chk_Summary.Location = new System.Drawing.Point(341, 13);
            this.chk_Summary.Name = "chk_Summary";
            this.chk_Summary.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Summary.Size = new System.Drawing.Size(180, 23);
            this.chk_Summary.TabIndex = 8;
            this.chk_Summary.Text = "Summary";
            this.chk_Summary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Summary.UseVisualStyleBackColor = true;
            // 
            // msk_ToDate
            // 
            this.msk_ToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_ToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msk_ToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.msk_ToDate.Location = new System.Drawing.Point(111, 69);
            this.msk_ToDate.Mask = "00/00/0000";
            this.msk_ToDate.Name = "msk_ToDate";
            this.msk_ToDate.Size = new System.Drawing.Size(128, 26);
            this.msk_ToDate.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 57;
            this.label2.Text = "To Date";
            // 
            // msk_FromDate
            // 
            this.msk_FromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_FromDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msk_FromDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.msk_FromDate.Location = new System.Drawing.Point(111, 42);
            this.msk_FromDate.Mask = "00/00/0000";
            this.msk_FromDate.Name = "msk_FromDate";
            this.msk_FromDate.Size = new System.Drawing.Size(128, 26);
            this.msk_FromDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 20);
            this.label1.TabIndex = 55;
            this.label1.Text = "From Date";
            // 
            // chk_Horizontal
            // 
            this.chk_Horizontal.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Horizontal.Location = new System.Drawing.Point(341, 36);
            this.chk_Horizontal.Name = "chk_Horizontal";
            this.chk_Horizontal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Horizontal.Size = new System.Drawing.Size(180, 23);
            this.chk_Horizontal.TabIndex = 9;
            this.chk_Horizontal.Text = "Horizontal";
            this.chk_Horizontal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Horizontal.UseVisualStyleBackColor = true;
            // 
            // chk_Class
            // 
            this.chk_Class.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Class.Location = new System.Drawing.Point(341, 59);
            this.chk_Class.Name = "chk_Class";
            this.chk_Class.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Class.Size = new System.Drawing.Size(180, 23);
            this.chk_Class.TabIndex = 10;
            this.chk_Class.Text = "Document Class";
            this.chk_Class.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Class.UseVisualStyleBackColor = true;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(452, 9);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(83, 33);
            this.simpleButton1.TabIndex = 38;
            this.simpleButton1.Text = "&Cancel";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Location = new System.Drawing.Point(362, 9);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(89, 33);
            this.simpleButton2.TabIndex = 39;
            this.simpleButton2.Text = "&Show";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.simpleButton2);
            this.groupBox1.Controls.Add(this.simpleButton1);
            this.groupBox1.Location = new System.Drawing.Point(2, 239);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(536, 45);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            // 
            // FrmCreditNoteRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(541, 283);
            this.Controls.Add(this.gb_TBOptions);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmCreditNoteRegister";
            this.Text = "Credit Note Register";
            this.Load += new System.EventHandler(this.CreditNoteRegister_Load);
            this.gb_TBOptions.ResumeLayout(false);
            this.gb_TBOptions.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chk_IncludeSubedger;
        private System.Windows.Forms.GroupBox gb_TBOptions;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSysDateType;
        private System.Windows.Forms.ComboBox cmb_FilterFor;
        private System.Windows.Forms.Label lbl_FilterFor;
        private System.Windows.Forms.ComboBox cmb_InvoiceType;
        private System.Windows.Forms.Label lbl_InvoiceType;
        private System.Windows.Forms.Button btn_Class;
        private System.Windows.Forms.Label lbl_Branch;
        private System.Windows.Forms.ComboBox cmb_Branch;
        private System.Windows.Forms.Label lbl_Find;
        private System.Windows.Forms.CheckBox chk_Date;
        private System.Windows.Forms.CheckBox chk_SelectAll;
        private System.Windows.Forms.CheckBox chk_Remarks;
        private System.Windows.Forms.ComboBox cmb_GroupBy;
        private System.Windows.Forms.Label lbl_GroupBy;
        private System.Windows.Forms.ComboBox cmb_Currency;
        private System.Windows.Forms.Label lbl_Currency;
        private System.Windows.Forms.CheckBox chk_Summary;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chk_Horizontal;
        private System.Windows.Forms.CheckBox chk_Class;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private System.Windows.Forms.GroupBox groupBox1;
        private MrTextBox txt_Class;
        private MrTextBox txt_Find;
        private MrMaskedTextBox msk_ToDate;
        private MrMaskedTextBox msk_FromDate;
    }
}