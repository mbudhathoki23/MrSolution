using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.Sales_Register
{
    partial class FrmSalesDispatchOrderRegister
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
            this.btn_Show = new System.Windows.Forms.Button();
            this.gb_TBOptions = new System.Windows.Forms.GroupBox();
            this.chk_Class = new System.Windows.Forms.CheckBox();
            this.btn_Class = new System.Windows.Forms.Button();
            this.txt_Class = new MrTextBox();
            this.lbl_Class = new System.Windows.Forms.Label();
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
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSysDateType = new System.Windows.Forms.ComboBox();
            this.gb_TBOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Show
            // 
            this.btn_Show.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Show.Location = new System.Drawing.Point(439, 153);
            this.btn_Show.Name = "btn_Show";
            this.btn_Show.Size = new System.Drawing.Size(71, 29);
            this.btn_Show.TabIndex = 17;
            this.btn_Show.Text = "&Show";
            this.btn_Show.UseVisualStyleBackColor = true;
            // 
            // gb_TBOptions
            // 
            this.gb_TBOptions.Controls.Add(this.label4);
            this.gb_TBOptions.Controls.Add(this.cmbSysDateType);
            this.gb_TBOptions.Controls.Add(this.chk_Class);
            this.gb_TBOptions.Controls.Add(this.btn_Class);
            this.gb_TBOptions.Controls.Add(this.txt_Class);
            this.gb_TBOptions.Controls.Add(this.lbl_Class);
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
            this.gb_TBOptions.Location = new System.Drawing.Point(5, 2);
            this.gb_TBOptions.Name = "gb_TBOptions";
            this.gb_TBOptions.Size = new System.Drawing.Size(597, 145);
            this.gb_TBOptions.TabIndex = 16;
            this.gb_TBOptions.TabStop = false;
            // 
            // chk_Class
            // 
            this.chk_Class.Font = new System.Drawing.Font("Arial", 9F);
            this.chk_Class.Location = new System.Drawing.Point(425, 20);
            this.chk_Class.Name = "chk_Class";
            this.chk_Class.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Class.Size = new System.Drawing.Size(164, 19);
            this.chk_Class.TabIndex = 10;
            this.chk_Class.Text = "Document Class";
            this.chk_Class.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Class.UseVisualStyleBackColor = true;
            // 
            // btn_Class
            // 
            this.btn_Class.Location = new System.Drawing.Point(565, 43);
            this.btn_Class.Name = "btn_Class";
            this.btn_Class.Size = new System.Drawing.Size(25, 23);
            this.btn_Class.TabIndex = 12;
            this.btn_Class.TabStop = false;
            this.btn_Class.Text = "..";
            this.btn_Class.UseVisualStyleBackColor = true;
            // 
            // txt_Class
            // 
            this.txt_Class.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txt_Class.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Class.Location = new System.Drawing.Point(470, 44);
            this.txt_Class.MaxLength = 255;
            this.txt_Class.Name = "txt_Class";
            this.txt_Class.Size = new System.Drawing.Size(95, 20);
            this.txt_Class.TabIndex = 11;
            // 
            // lbl_Class
            // 
            this.lbl_Class.AutoSize = true;
            this.lbl_Class.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_Class.Location = new System.Drawing.Point(425, 47);
            this.lbl_Class.Name = "lbl_Class";
            this.lbl_Class.Size = new System.Drawing.Size(40, 15);
            this.lbl_Class.TabIndex = 169;
            this.lbl_Class.Text = "Class";
            // 
            // lbl_Branch
            // 
            this.lbl_Branch.AutoSize = true;
            this.lbl_Branch.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_Branch.Location = new System.Drawing.Point(10, 120);
            this.lbl_Branch.Name = "lbl_Branch";
            this.lbl_Branch.Size = new System.Drawing.Size(49, 15);
            this.lbl_Branch.TabIndex = 77;
            this.lbl_Branch.Text = "Branch ";
            // 
            // cmb_Branch
            // 
            this.cmb_Branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Branch.FormattingEnabled = true;
            this.cmb_Branch.Location = new System.Drawing.Point(88, 118);
            this.cmb_Branch.Name = "cmb_Branch";
            this.cmb_Branch.Size = new System.Drawing.Size(126, 21);
            this.cmb_Branch.TabIndex = 5;
            // 
            // txt_Find
            // 
            this.txt_Find.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Find.Location = new System.Drawing.Point(296, 94);
            this.txt_Find.Name = "txt_Find";
            this.txt_Find.Size = new System.Drawing.Size(107, 20);
            this.txt_Find.TabIndex = 9;
            // 
            // lbl_Find
            // 
            this.lbl_Find.AutoSize = true;
            this.lbl_Find.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_Find.Location = new System.Drawing.Point(239, 97);
            this.lbl_Find.Name = "lbl_Find";
            this.lbl_Find.Size = new System.Drawing.Size(31, 15);
            this.lbl_Find.TabIndex = 71;
            this.lbl_Find.Text = "Find";
            // 
            // chk_Date
            // 
            this.chk_Date.Font = new System.Drawing.Font("Arial", 9F);
            this.chk_Date.Location = new System.Drawing.Point(425, 71);
            this.chk_Date.Name = "chk_Date";
            this.chk_Date.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Date.Size = new System.Drawing.Size(165, 19);
            this.chk_Date.TabIndex = 13;
            this.chk_Date.Text = "Date";
            this.chk_Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Date.UseVisualStyleBackColor = true;
            // 
            // chk_SelectAll
            // 
            this.chk_SelectAll.Font = new System.Drawing.Font("Arial", 9F);
            this.chk_SelectAll.Location = new System.Drawing.Point(425, 97);
            this.chk_SelectAll.Name = "chk_SelectAll";
            this.chk_SelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_SelectAll.Size = new System.Drawing.Size(165, 19);
            this.chk_SelectAll.TabIndex = 14;
            this.chk_SelectAll.Text = "Select All";
            this.chk_SelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_SelectAll.UseVisualStyleBackColor = true;
            // 
            // chk_Remarks
            // 
            this.chk_Remarks.Font = new System.Drawing.Font("Arial", 9F);
            this.chk_Remarks.Location = new System.Drawing.Point(240, 43);
            this.chk_Remarks.Name = "chk_Remarks";
            this.chk_Remarks.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Remarks.Size = new System.Drawing.Size(161, 19);
            this.chk_Remarks.TabIndex = 7;
            this.chk_Remarks.Text = "Remarks";
            this.chk_Remarks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Remarks.UseVisualStyleBackColor = true;
            // 
            // cmb_GroupBy
            // 
            this.cmb_GroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.cmb_GroupBy.Location = new System.Drawing.Point(88, 92);
            this.cmb_GroupBy.Name = "cmb_GroupBy";
            this.cmb_GroupBy.Size = new System.Drawing.Size(126, 21);
            this.cmb_GroupBy.TabIndex = 4;
            // 
            // lbl_GroupBy
            // 
            this.lbl_GroupBy.AutoSize = true;
            this.lbl_GroupBy.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_GroupBy.Location = new System.Drawing.Point(10, 94);
            this.lbl_GroupBy.Name = "lbl_GroupBy";
            this.lbl_GroupBy.Size = new System.Drawing.Size(57, 15);
            this.lbl_GroupBy.TabIndex = 64;
            this.lbl_GroupBy.Text = "Group By";
            // 
            // cmb_Currency
            // 
            this.cmb_Currency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Currency.FormattingEnabled = true;
            this.cmb_Currency.Items.AddRange(new object[] {
            "Local",
            "Foreign",
            "Both"});
            this.cmb_Currency.Location = new System.Drawing.Point(296, 67);
            this.cmb_Currency.Name = "cmb_Currency";
            this.cmb_Currency.Size = new System.Drawing.Size(105, 21);
            this.cmb_Currency.TabIndex = 8;
            // 
            // lbl_Currency
            // 
            this.lbl_Currency.AutoSize = true;
            this.lbl_Currency.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_Currency.Location = new System.Drawing.Point(239, 69);
            this.lbl_Currency.Name = "lbl_Currency";
            this.lbl_Currency.Size = new System.Drawing.Size(56, 15);
            this.lbl_Currency.TabIndex = 62;
            this.lbl_Currency.Text = "Currency";
            // 
            // chk_Summary
            // 
            this.chk_Summary.Checked = true;
            this.chk_Summary.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Summary.Font = new System.Drawing.Font("Arial", 9F);
            this.chk_Summary.Location = new System.Drawing.Point(240, 19);
            this.chk_Summary.Name = "chk_Summary";
            this.chk_Summary.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Summary.Size = new System.Drawing.Size(161, 19);
            this.chk_Summary.TabIndex = 6;
            this.chk_Summary.Text = "Summary";
            this.chk_Summary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Summary.UseVisualStyleBackColor = true;
            // 
            // msk_ToDate
            // 
            this.msk_ToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_ToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msk_ToDate.Location = new System.Drawing.Point(88, 67);
            this.msk_ToDate.Mask = "00/00/0000";
            this.msk_ToDate.Name = "msk_ToDate";
            this.msk_ToDate.Size = new System.Drawing.Size(126, 20);
            this.msk_ToDate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F);
            this.label2.Location = new System.Drawing.Point(10, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 15);
            this.label2.TabIndex = 57;
            this.label2.Text = "To Date";
            // 
            // msk_FromDate
            // 
            this.msk_FromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_FromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msk_FromDate.Location = new System.Drawing.Point(88, 42);
            this.msk_FromDate.Mask = "00/00/0000";
            this.msk_FromDate.Name = "msk_FromDate";
            this.msk_FromDate.Size = new System.Drawing.Size(126, 20);
            this.msk_FromDate.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F);
            this.label1.Location = new System.Drawing.Point(10, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 15);
            this.label1.TabIndex = 55;
            this.label1.Text = "From Date";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Location = new System.Drawing.Point(516, 153);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(72, 29);
            this.btn_Cancel.TabIndex = 33;
            this.btn_Cancel.Text = "&Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F);
            this.label4.Location = new System.Drawing.Point(10, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 15);
            this.label4.TabIndex = 190;
            this.label4.Text = "Date Type";
            // 
            // cmbSysDateType
            // 
            this.cmbSysDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.cmbSysDateType.Location = new System.Drawing.Point(88, 13);
            this.cmbSysDateType.Name = "cmbSysDateType";
            this.cmbSysDateType.Size = new System.Drawing.Size(126, 21);
            this.cmbSysDateType.TabIndex = 189;
            this.cmbSysDateType.SelectedIndexChanged += new System.EventHandler(this.cmbSysDateType_SelectedIndexChanged);
            // 
            // SalesDispatchOrderRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 184);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Show);
            this.Controls.Add(this.gb_TBOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "SalesDispatchOrderRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Dispatch Order Register";
            this.Load += new System.EventHandler(this.SalesDispatchOrderRegister_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SalesDispatchOrderRegister_KeyPress);
            this.gb_TBOptions.ResumeLayout(false);
            this.gb_TBOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Show;
        private System.Windows.Forms.GroupBox gb_TBOptions;
        private System.Windows.Forms.CheckBox chk_Class;
        private System.Windows.Forms.Button btn_Class;
        private System.Windows.Forms.TextBox txt_Class;
        private System.Windows.Forms.Label lbl_Class;
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
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSysDateType;
    }
}