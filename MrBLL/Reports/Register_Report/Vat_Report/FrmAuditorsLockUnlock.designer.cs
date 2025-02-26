using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.Vat_Report
{
    partial class FrmAuditorsLockUnlock
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
            this.gb_TBOptions = new System.Windows.Forms.GroupBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Show = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_OpenFor = new System.Windows.Forms.Label();
            this.cmb_OpenFor = new System.Windows.Forms.ComboBox();
            this.lbl_Branch = new System.Windows.Forms.Label();
            this.cmb_Branch = new System.Windows.Forms.ComboBox();
            this.txt_Find = new MrTextBox();
            this.lbl_Find = new System.Windows.Forms.Label();
            this.chk_Date = new System.Windows.Forms.CheckBox();
            this.chklstbox_DayBook = new System.Windows.Forms.CheckedListBox();
            this.cmb_Currency = new System.Windows.Forms.ComboBox();
            this.lbl_Currency = new System.Windows.Forms.Label();
            this.chk_SubLedger = new System.Windows.Forms.CheckBox();
            this.chk_Remarks = new System.Windows.Forms.CheckBox();
            this.msk_ToDate = new MrMaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.msk_FromDate = new MrMaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gb_TBOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_TBOptions
            // 
            this.gb_TBOptions.Controls.Add(this.simpleButton1);
            this.gb_TBOptions.Controls.Add(this.btn_Show);
            this.gb_TBOptions.Controls.Add(this.lbl_OpenFor);
            this.gb_TBOptions.Controls.Add(this.cmb_OpenFor);
            this.gb_TBOptions.Controls.Add(this.lbl_Branch);
            this.gb_TBOptions.Controls.Add(this.cmb_Branch);
            this.gb_TBOptions.Controls.Add(this.txt_Find);
            this.gb_TBOptions.Controls.Add(this.lbl_Find);
            this.gb_TBOptions.Controls.Add(this.chk_Date);
            this.gb_TBOptions.Controls.Add(this.chklstbox_DayBook);
            this.gb_TBOptions.Controls.Add(this.cmb_Currency);
            this.gb_TBOptions.Controls.Add(this.lbl_Currency);
            this.gb_TBOptions.Controls.Add(this.chk_SubLedger);
            this.gb_TBOptions.Controls.Add(this.chk_Remarks);
            this.gb_TBOptions.Controls.Add(this.msk_ToDate);
            this.gb_TBOptions.Controls.Add(this.label2);
            this.gb_TBOptions.Controls.Add(this.msk_FromDate);
            this.gb_TBOptions.Controls.Add(this.label1);
            this.gb_TBOptions.Location = new System.Drawing.Point(6, -9);
            this.gb_TBOptions.Margin = new System.Windows.Forms.Padding(4);
            this.gb_TBOptions.Name = "gb_TBOptions";
            this.gb_TBOptions.Padding = new System.Windows.Forms.Padding(4);
            this.gb_TBOptions.Size = new System.Drawing.Size(564, 312);
            this.gb_TBOptions.TabIndex = 0;
            this.gb_TBOptions.TabStop = false;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(194, 268);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(96, 36);
            this.simpleButton1.TabIndex = 77;
            this.simpleButton1.Text = "&Cancel";
            // 
            // btn_Show
            // 
            this.btn_Show.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Show.Appearance.Options.UseFont = true;
            this.btn_Show.Location = new System.Drawing.Point(93, 268);
            this.btn_Show.Name = "btn_Show";
            this.btn_Show.Size = new System.Drawing.Size(96, 36);
            this.btn_Show.TabIndex = 76;
            this.btn_Show.Text = "&Show";
            this.btn_Show.Click += new System.EventHandler(this.btn_Show_Click);
            // 
            // lbl_OpenFor
            // 
            this.lbl_OpenFor.AutoSize = true;
            this.lbl_OpenFor.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_OpenFor.Location = new System.Drawing.Point(6, 130);
            this.lbl_OpenFor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_OpenFor.Name = "lbl_OpenFor";
            this.lbl_OpenFor.Size = new System.Drawing.Size(83, 20);
            this.lbl_OpenFor.TabIndex = 75;
            this.lbl_OpenFor.Text = "Open For";
            // 
            // cmb_OpenFor
            // 
            this.cmb_OpenFor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmb_OpenFor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_OpenFor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_OpenFor.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.cmb_OpenFor.FormattingEnabled = true;
            this.cmb_OpenFor.Items.AddRange(new object[] {
            "Both",
            "Lock",
            "UnLock"});
            this.cmb_OpenFor.Location = new System.Drawing.Point(102, 129);
            this.cmb_OpenFor.Margin = new System.Windows.Forms.Padding(4);
            this.cmb_OpenFor.Name = "cmb_OpenFor";
            this.cmb_OpenFor.Size = new System.Drawing.Size(183, 27);
            this.cmb_OpenFor.TabIndex = 5;
            // 
            // lbl_Branch
            // 
            this.lbl_Branch.AutoSize = true;
            this.lbl_Branch.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_Branch.Location = new System.Drawing.Point(6, 101);
            this.lbl_Branch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Branch.Name = "lbl_Branch";
            this.lbl_Branch.Size = new System.Drawing.Size(72, 20);
            this.lbl_Branch.TabIndex = 73;
            this.lbl_Branch.Text = "Branch ";
            // 
            // cmb_Branch
            // 
            this.cmb_Branch.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmb_Branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Branch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_Branch.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.cmb_Branch.FormattingEnabled = true;
            this.cmb_Branch.Location = new System.Drawing.Point(102, 100);
            this.cmb_Branch.Margin = new System.Windows.Forms.Padding(4);
            this.cmb_Branch.Name = "cmb_Branch";
            this.cmb_Branch.Size = new System.Drawing.Size(183, 27);
            this.cmb_Branch.TabIndex = 4;
            // 
            // txt_Find
            // 
            this.txt_Find.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Find.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.txt_Find.Location = new System.Drawing.Point(102, 158);
            this.txt_Find.Margin = new System.Windows.Forms.Padding(4);
            this.txt_Find.Name = "txt_Find";
            this.txt_Find.Size = new System.Drawing.Size(184, 26);
            this.txt_Find.TabIndex = 6;
            this.txt_Find.Enter += new System.EventHandler(this.txt_Find_Enter);
            this.txt_Find.Leave += new System.EventHandler(this.txt_Find_Leave);
            // 
            // lbl_Find
            // 
            this.lbl_Find.AutoSize = true;
            this.lbl_Find.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_Find.Location = new System.Drawing.Point(6, 158);
            this.lbl_Find.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Find.Name = "lbl_Find";
            this.lbl_Find.Size = new System.Drawing.Size(45, 20);
            this.lbl_Find.TabIndex = 71;
            this.lbl_Find.Text = "Find";
            // 
            // chk_Date
            // 
            this.chk_Date.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.chk_Date.Location = new System.Drawing.Point(6, 243);
            this.chk_Date.Margin = new System.Windows.Forms.Padding(4);
            this.chk_Date.Name = "chk_Date";
            this.chk_Date.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Date.Size = new System.Drawing.Size(181, 22);
            this.chk_Date.TabIndex = 9;
            this.chk_Date.Text = "Miti";
            this.chk_Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Date.UseVisualStyleBackColor = true;
            // 
            // chklstbox_DayBook
            // 
            this.chklstbox_DayBook.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.chklstbox_DayBook.FormattingEnabled = true;
            this.chklstbox_DayBook.Location = new System.Drawing.Point(294, 11);
            this.chklstbox_DayBook.Margin = new System.Windows.Forms.Padding(4);
            this.chklstbox_DayBook.Name = "chklstbox_DayBook";
            this.chklstbox_DayBook.Size = new System.Drawing.Size(268, 298);
            this.chklstbox_DayBook.TabIndex = 10;
            // 
            // cmb_Currency
            // 
            this.cmb_Currency.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmb_Currency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Currency.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_Currency.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.cmb_Currency.FormattingEnabled = true;
            this.cmb_Currency.Items.AddRange(new object[] {
            "Local",
            "Foreign",
            "Both"});
            this.cmb_Currency.Location = new System.Drawing.Point(102, 71);
            this.cmb_Currency.Margin = new System.Windows.Forms.Padding(4);
            this.cmb_Currency.Name = "cmb_Currency";
            this.cmb_Currency.Size = new System.Drawing.Size(183, 27);
            this.cmb_Currency.TabIndex = 3;
            // 
            // lbl_Currency
            // 
            this.lbl_Currency.AutoSize = true;
            this.lbl_Currency.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_Currency.Location = new System.Drawing.Point(6, 72);
            this.lbl_Currency.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Currency.Name = "lbl_Currency";
            this.lbl_Currency.Size = new System.Drawing.Size(83, 20);
            this.lbl_Currency.TabIndex = 60;
            this.lbl_Currency.Text = "Currency";
            // 
            // chk_SubLedger
            // 
            this.chk_SubLedger.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.chk_SubLedger.Location = new System.Drawing.Point(6, 187);
            this.chk_SubLedger.Margin = new System.Windows.Forms.Padding(4);
            this.chk_SubLedger.Name = "chk_SubLedger";
            this.chk_SubLedger.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_SubLedger.Size = new System.Drawing.Size(181, 24);
            this.chk_SubLedger.TabIndex = 7;
            this.chk_SubLedger.Text = "Sub Ledger";
            this.chk_SubLedger.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_SubLedger.UseVisualStyleBackColor = true;
            // 
            // chk_Remarks
            // 
            this.chk_Remarks.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.chk_Remarks.Location = new System.Drawing.Point(6, 216);
            this.chk_Remarks.Margin = new System.Windows.Forms.Padding(4);
            this.chk_Remarks.Name = "chk_Remarks";
            this.chk_Remarks.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Remarks.Size = new System.Drawing.Size(181, 22);
            this.chk_Remarks.TabIndex = 8;
            this.chk_Remarks.Text = "Remarks";
            this.chk_Remarks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Remarks.UseVisualStyleBackColor = true;
            // 
            // msk_ToDate
            // 
            this.msk_ToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_ToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.msk_ToDate.Location = new System.Drawing.Point(102, 43);
            this.msk_ToDate.Margin = new System.Windows.Forms.Padding(4);
            this.msk_ToDate.Mask = "00/00/0000";
            this.msk_ToDate.Name = "msk_ToDate";
            this.msk_ToDate.Size = new System.Drawing.Size(123, 26);
            this.msk_ToDate.TabIndex = 2;
            this.msk_ToDate.Enter += new System.EventHandler(this.msk_ToDate_Enter);
            this.msk_ToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msk_ToDate_KeyDown);
            this.msk_ToDate.Leave += new System.EventHandler(this.msk_ToDate_Leave);
            this.msk_ToDate.Validated += new System.EventHandler(this.msk_ToDate_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label2.Location = new System.Drawing.Point(6, 43);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 57;
            this.label2.Text = "To Date";
            // 
            // msk_FromDate
            // 
            this.msk_FromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_FromDate.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.msk_FromDate.Location = new System.Drawing.Point(102, 15);
            this.msk_FromDate.Margin = new System.Windows.Forms.Padding(4);
            this.msk_FromDate.Mask = "00/00/0000";
            this.msk_FromDate.Name = "msk_FromDate";
            this.msk_FromDate.Size = new System.Drawing.Size(123, 26);
            this.msk_FromDate.TabIndex = 1;
            this.msk_FromDate.Enter += new System.EventHandler(this.msk_FromDate_Enter);
            this.msk_FromDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msk_FromDate_KeyDown);
            this.msk_FromDate.Leave += new System.EventHandler(this.msk_FromDate_Leave);
            this.msk_FromDate.Validated += new System.EventHandler(this.msk_FromDate_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label1.Location = new System.Drawing.Point(6, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 20);
            this.label1.TabIndex = 55;
            this.label1.Text = "From Date";
            // 
            // FrmAuditorsLockUnlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(577, 303);
            this.Controls.Add(this.gb_TBOptions);
            this.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmAuditorsLockUnlock";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auditors Lock Unlock";
            this.Load += new System.EventHandler(this.FrmAuditorsLockUnlock_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmAuditorsLockUnlock_KeyPress);
            this.gb_TBOptions.ResumeLayout(false);
            this.gb_TBOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gb_TBOptions;
        private System.Windows.Forms.Label lbl_Branch;
        private System.Windows.Forms.ComboBox cmb_Branch;
        private System.Windows.Forms.TextBox txt_Find;
        private System.Windows.Forms.Label lbl_Find;
        private System.Windows.Forms.CheckBox chk_Date;
        private System.Windows.Forms.CheckedListBox chklstbox_DayBook;
        private System.Windows.Forms.ComboBox cmb_Currency;
        private System.Windows.Forms.Label lbl_Currency;
        private System.Windows.Forms.CheckBox chk_SubLedger;
        private System.Windows.Forms.CheckBox chk_Remarks;
        private System.Windows.Forms.MaskedTextBox msk_ToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox msk_FromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_OpenFor;
        private System.Windows.Forms.ComboBox cmb_OpenFor;
        private DevExpress.XtraEditors.SimpleButton btn_Show;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}