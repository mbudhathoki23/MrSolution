using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.Register_Report
{
    partial class FrmPartyAnalysis
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
			this.label4 = new System.Windows.Forms.Label();
			this.cmbSysDateType = new System.Windows.Forms.ComboBox();
			this.lbl_Branch = new System.Windows.Forms.Label();
			this.cmb_Branch = new System.Windows.Forms.ComboBox();
			this.lbl_PartyType = new System.Windows.Forms.Label();
			this.rb_Vendor = new System.Windows.Forms.RadioButton();
			this.rb_Customer = new System.Windows.Forms.RadioButton();
			this.chk_Date = new System.Windows.Forms.CheckBox();
			this.chk_AllLedger = new System.Windows.Forms.CheckBox();
			this.msk_ToDate = new MrMaskedTextBox();
			this.lbl_ToDate = new System.Windows.Forms.Label();
			this.msk_FromDate = new MrMaskedTextBox();
			this.lbl_FromDate = new System.Windows.Forms.Label();
			this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			this.gb_TBOptions.SuspendLayout();
			this.SuspendLayout();
			// 
			// gb_TBOptions
			// 
			this.gb_TBOptions.Controls.Add(this.label4);
			this.gb_TBOptions.Controls.Add(this.cmbSysDateType);
			this.gb_TBOptions.Controls.Add(this.lbl_Branch);
			this.gb_TBOptions.Controls.Add(this.cmb_Branch);
			this.gb_TBOptions.Controls.Add(this.lbl_PartyType);
			this.gb_TBOptions.Controls.Add(this.rb_Vendor);
			this.gb_TBOptions.Controls.Add(this.rb_Customer);
			this.gb_TBOptions.Controls.Add(this.chk_Date);
			this.gb_TBOptions.Controls.Add(this.chk_AllLedger);
			this.gb_TBOptions.Controls.Add(this.msk_ToDate);
			this.gb_TBOptions.Controls.Add(this.lbl_ToDate);
			this.gb_TBOptions.Controls.Add(this.msk_FromDate);
			this.gb_TBOptions.Controls.Add(this.lbl_FromDate);
			this.gb_TBOptions.Location = new System.Drawing.Point(8, -6);
			this.gb_TBOptions.Name = "gb_TBOptions";
			this.gb_TBOptions.Size = new System.Drawing.Size(484, 151);
			this.gb_TBOptions.TabIndex = 1;
			this.gb_TBOptions.TabStop = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(6, 38);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 20);
			this.label4.TabIndex = 194;
			this.label4.Text = "Date Type";
			// 
			// cmbSysDateType
			// 
			this.cmbSysDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSysDateType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
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
			this.cmbSysDateType.Location = new System.Drawing.Point(107, 36);
			this.cmbSysDateType.Name = "cmbSysDateType";
			this.cmbSysDateType.Size = new System.Drawing.Size(205, 28);
			this.cmbSysDateType.TabIndex = 193;
			// 
			// lbl_Branch
			// 
			this.lbl_Branch.AutoSize = true;
			this.lbl_Branch.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_Branch.Location = new System.Drawing.Point(6, 124);
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
			this.cmb_Branch.Location = new System.Drawing.Point(107, 119);
			this.cmb_Branch.Name = "cmb_Branch";
			this.cmb_Branch.Size = new System.Drawing.Size(205, 28);
			this.cmb_Branch.TabIndex = 6;
			// 
			// lbl_PartyType
			// 
			this.lbl_PartyType.AutoSize = true;
			this.lbl_PartyType.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_PartyType.Location = new System.Drawing.Point(10, 10);
			this.lbl_PartyType.Name = "lbl_PartyType";
			this.lbl_PartyType.Size = new System.Drawing.Size(92, 20);
			this.lbl_PartyType.TabIndex = 65;
			this.lbl_PartyType.Text = "Party Type";
			// 
			// rb_Vendor
			// 
			this.rb_Vendor.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rb_Vendor.Location = new System.Drawing.Point(330, 4);
			this.rb_Vendor.Name = "rb_Vendor";
			this.rb_Vendor.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.rb_Vendor.Size = new System.Drawing.Size(138, 25);
			this.rb_Vendor.TabIndex = 3;
			this.rb_Vendor.TabStop = true;
			this.rb_Vendor.Text = "Vendor";
			this.rb_Vendor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.rb_Vendor.UseVisualStyleBackColor = true;
			// 
			// rb_Customer
			// 
			this.rb_Customer.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rb_Customer.Location = new System.Drawing.Point(182, 3);
			this.rb_Customer.Name = "rb_Customer";
			this.rb_Customer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.rb_Customer.Size = new System.Drawing.Size(115, 27);
			this.rb_Customer.TabIndex = 2;
			this.rb_Customer.TabStop = true;
			this.rb_Customer.Text = "Customer";
			this.rb_Customer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.rb_Customer.UseVisualStyleBackColor = true;
			// 
			// chk_Date
			// 
			this.chk_Date.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chk_Date.Location = new System.Drawing.Point(334, 39);
			this.chk_Date.Name = "chk_Date";
			this.chk_Date.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.chk_Date.Size = new System.Drawing.Size(132, 25);
			this.chk_Date.TabIndex = 7;
			this.chk_Date.Text = "Date";
			this.chk_Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chk_Date.UseVisualStyleBackColor = true;
			// 
			// chk_AllLedger
			// 
			this.chk_AllLedger.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chk_AllLedger.Location = new System.Drawing.Point(334, 66);
			this.chk_AllLedger.Name = "chk_AllLedger";
			this.chk_AllLedger.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.chk_AllLedger.Size = new System.Drawing.Size(132, 25);
			this.chk_AllLedger.TabIndex = 8;
			this.chk_AllLedger.Text = "All Ledger";
			this.chk_AllLedger.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chk_AllLedger.UseVisualStyleBackColor = true;
			// 
			// msk_ToDate
			// 
			this.msk_ToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.msk_ToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.msk_ToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
			this.msk_ToDate.Location = new System.Drawing.Point(107, 92);
			this.msk_ToDate.Mask = "00/00/0000";
			this.msk_ToDate.Name = "msk_ToDate";
			this.msk_ToDate.Size = new System.Drawing.Size(128, 26);
			this.msk_ToDate.TabIndex = 5;
			this.msk_ToDate.Enter += new System.EventHandler(this.msk_ToDate_Enter);
			this.msk_ToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msk_ToDate_KeyDown);
			this.msk_ToDate.Leave += new System.EventHandler(this.msk_ToDate_Leave);
			this.msk_ToDate.Validated += new System.EventHandler(this.msk_ToDate_Validated);
			// 
			// lbl_ToDate
			// 
			this.lbl_ToDate.AutoSize = true;
			this.lbl_ToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_ToDate.Location = new System.Drawing.Point(6, 94);
			this.lbl_ToDate.Name = "lbl_ToDate";
			this.lbl_ToDate.Size = new System.Drawing.Size(69, 20);
			this.lbl_ToDate.TabIndex = 57;
			this.lbl_ToDate.Text = "To Date";
			// 
			// msk_FromDate
			// 
			this.msk_FromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.msk_FromDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.msk_FromDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
			this.msk_FromDate.Location = new System.Drawing.Point(107, 67);
			this.msk_FromDate.Mask = "00/00/0000";
			this.msk_FromDate.Name = "msk_FromDate";
			this.msk_FromDate.Size = new System.Drawing.Size(128, 26);
			this.msk_FromDate.TabIndex = 4;
			this.msk_FromDate.Enter += new System.EventHandler(this.msk_FromDate_Enter);
			this.msk_FromDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msk_FromDate_KeyDown);
			this.msk_FromDate.Leave += new System.EventHandler(this.msk_FromDate_Leave);
			this.msk_FromDate.Validated += new System.EventHandler(this.msk_FromDate_Validated);
			// 
			// lbl_FromDate
			// 
			this.lbl_FromDate.AutoSize = true;
			this.lbl_FromDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_FromDate.Location = new System.Drawing.Point(6, 69);
			this.lbl_FromDate.Name = "lbl_FromDate";
			this.lbl_FromDate.Size = new System.Drawing.Size(92, 20);
			this.lbl_FromDate.TabIndex = 55;
			this.lbl_FromDate.Text = "From Date";
			// 
			// btn_Cancel
			// 
			this.btn_Cancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 16F);
			this.btn_Cancel.Appearance.Options.UseFont = true;
			this.btn_Cancel.Location = new System.Drawing.Point(406, 145);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Size = new System.Drawing.Size(86, 37);
			this.btn_Cancel.TabIndex = 13;
			this.btn_Cancel.Text = "&Cancel";
			// 
			// simpleButton1
			// 
			this.simpleButton1.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 16F);
			this.simpleButton1.Appearance.Options.UseFont = true;
			this.simpleButton1.Location = new System.Drawing.Point(300, 145);
			this.simpleButton1.Name = "simpleButton1";
			this.simpleButton1.Size = new System.Drawing.Size(100, 37);
			this.simpleButton1.TabIndex = 12;
			this.simpleButton1.Text = "&Show";
			this.simpleButton1.Click += new System.EventHandler(this.btn_Show_Click);
			// 
			// FrmPartyAnalysis
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.ClientSize = new System.Drawing.Size(493, 182);
			this.Controls.Add(this.btn_Cancel);
			this.Controls.Add(this.simpleButton1);
			this.Controls.Add(this.gb_TBOptions);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "FrmPartyAnalysis";
			this.Text = "Party Analysis";
			this.Load += new System.EventHandler(this.PartyAnalysis_Load);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PartyAnalysis_KeyPress);
			this.gb_TBOptions.ResumeLayout(false);
			this.gb_TBOptions.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gb_TBOptions;
        private System.Windows.Forms.Label lbl_Branch;
        private System.Windows.Forms.ComboBox cmb_Branch;
        private System.Windows.Forms.Label lbl_PartyType;
        private System.Windows.Forms.RadioButton rb_Vendor;
        private System.Windows.Forms.RadioButton rb_Customer;
        private System.Windows.Forms.CheckBox chk_Date;
        private System.Windows.Forms.CheckBox chk_AllLedger;
        private System.Windows.Forms.MaskedTextBox msk_ToDate;
        private System.Windows.Forms.Label lbl_ToDate;
        private System.Windows.Forms.MaskedTextBox msk_FromDate;
        private System.Windows.Forms.Label lbl_FromDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSysDateType;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}