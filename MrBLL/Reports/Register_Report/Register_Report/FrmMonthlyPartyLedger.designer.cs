using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.Register_Report
{
    partial class FrmMonthlyPartyLedger
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
            this.chk_IncludeZeroBal = new System.Windows.Forms.CheckBox();
            this.lbl_Branch = new System.Windows.Forms.Label();
            this.cmb_Branch = new System.Windows.Forms.ComboBox();
            this.lbl_PartyType = new System.Windows.Forms.Label();
            this.rb_Vendor = new System.Windows.Forms.RadioButton();
            this.rb_Customer = new System.Windows.Forms.RadioButton();
            this.chk_Date = new System.Windows.Forms.CheckBox();
            this.chk_SubLedger = new System.Windows.Forms.CheckBox();
            this.cmb_GroupBy = new System.Windows.Forms.ComboBox();
            this.lbl_GroupBy = new System.Windows.Forms.Label();
            this.chk_AllLedger = new System.Windows.Forms.CheckBox();
            this.msk_ToDate = new MrMaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.msk_FromDate = new MrMaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Show = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.gb_TBOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_TBOptions
            // 
            this.gb_TBOptions.Controls.Add(this.label4);
            this.gb_TBOptions.Controls.Add(this.cmbSysDateType);
            this.gb_TBOptions.Controls.Add(this.chk_IncludeZeroBal);
            this.gb_TBOptions.Controls.Add(this.lbl_Branch);
            this.gb_TBOptions.Controls.Add(this.cmb_Branch);
            this.gb_TBOptions.Controls.Add(this.lbl_PartyType);
            this.gb_TBOptions.Controls.Add(this.rb_Vendor);
            this.gb_TBOptions.Controls.Add(this.rb_Customer);
            this.gb_TBOptions.Controls.Add(this.chk_Date);
            this.gb_TBOptions.Controls.Add(this.chk_SubLedger);
            this.gb_TBOptions.Controls.Add(this.cmb_GroupBy);
            this.gb_TBOptions.Controls.Add(this.lbl_GroupBy);
            this.gb_TBOptions.Controls.Add(this.chk_AllLedger);
            this.gb_TBOptions.Controls.Add(this.msk_ToDate);
            this.gb_TBOptions.Controls.Add(this.label2);
            this.gb_TBOptions.Controls.Add(this.msk_FromDate);
            this.gb_TBOptions.Controls.Add(this.label1);
            this.gb_TBOptions.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_TBOptions.Location = new System.Drawing.Point(1, 0);
            this.gb_TBOptions.Name = "gb_TBOptions";
            this.gb_TBOptions.Size = new System.Drawing.Size(524, 176);
            this.gb_TBOptions.TabIndex = 21;
            this.gb_TBOptions.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label4.Location = new System.Drawing.Point(11, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 20);
            this.label4.TabIndex = 89;
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
            this.cmbSysDateType.Location = new System.Drawing.Point(130, 30);
            this.cmbSysDateType.Name = "cmbSysDateType";
            this.cmbSysDateType.Size = new System.Drawing.Size(189, 28);
            this.cmbSysDateType.TabIndex = 0;
            this.cmbSysDateType.SelectedIndexChanged += new System.EventHandler(this.cmbSysDateType_SelectedIndexChanged);
            this.cmbSysDateType.Enter += new System.EventHandler(this.cmbSysDateType_Enter);
            this.cmbSysDateType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSysDateType_KeyDown);
            this.cmbSysDateType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbSysDateType_KeyPress);
            this.cmbSysDateType.Leave += new System.EventHandler(this.cmbSysDateType_Leave);
            // 
            // chk_IncludeZeroBal
            // 
            this.chk_IncludeZeroBal.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.chk_IncludeZeroBal.Location = new System.Drawing.Point(338, 58);
            this.chk_IncludeZeroBal.Name = "chk_IncludeZeroBal";
            this.chk_IncludeZeroBal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_IncludeZeroBal.Size = new System.Drawing.Size(176, 24);
            this.chk_IncludeZeroBal.TabIndex = 6;
            this.chk_IncludeZeroBal.Text = "Include Zero Bal";
            this.chk_IncludeZeroBal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_IncludeZeroBal.UseVisualStyleBackColor = true;
            this.chk_IncludeZeroBal.Visible = false;
            // 
            // lbl_Branch
            // 
            this.lbl_Branch.AutoSize = true;
            this.lbl_Branch.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_Branch.Location = new System.Drawing.Point(11, 146);
            this.lbl_Branch.Name = "lbl_Branch";
            this.lbl_Branch.Size = new System.Drawing.Size(72, 20);
            this.lbl_Branch.TabIndex = 75;
            this.lbl_Branch.Text = "Branch ";
            // 
            // cmb_Branch
            // 
            this.cmb_Branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Branch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_Branch.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.cmb_Branch.FormattingEnabled = true;
            this.cmb_Branch.Location = new System.Drawing.Point(130, 143);
            this.cmb_Branch.Name = "cmb_Branch";
            this.cmb_Branch.Size = new System.Drawing.Size(188, 28);
            this.cmb_Branch.TabIndex = 4;
            // 
            // lbl_PartyType
            // 
            this.lbl_PartyType.AutoSize = true;
            this.lbl_PartyType.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_PartyType.Location = new System.Drawing.Point(8, 2);
            this.lbl_PartyType.Name = "lbl_PartyType";
            this.lbl_PartyType.Size = new System.Drawing.Size(92, 20);
            this.lbl_PartyType.TabIndex = 65;
            this.lbl_PartyType.Text = "Party Type";
            // 
            // rb_Vendor
            // 
            this.rb_Vendor.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.rb_Vendor.Location = new System.Drawing.Point(324, 2);
            this.rb_Vendor.Name = "rb_Vendor";
            this.rb_Vendor.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rb_Vendor.Size = new System.Drawing.Size(135, 23);
            this.rb_Vendor.TabIndex = 0;
            this.rb_Vendor.TabStop = true;
            this.rb_Vendor.Text = "Vendor";
            this.rb_Vendor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rb_Vendor.UseVisualStyleBackColor = true;
            // 
            // rb_Customer
            // 
            this.rb_Customer.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.rb_Customer.Location = new System.Drawing.Point(119, 2);
            this.rb_Customer.Name = "rb_Customer";
            this.rb_Customer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rb_Customer.Size = new System.Drawing.Size(122, 23);
            this.rb_Customer.TabIndex = 0;
            this.rb_Customer.TabStop = true;
            this.rb_Customer.Text = "Customer";
            this.rb_Customer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rb_Customer.UseVisualStyleBackColor = true;
            // 
            // chk_Date
            // 
            this.chk_Date.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.chk_Date.Location = new System.Drawing.Point(338, 32);
            this.chk_Date.Name = "chk_Date";
            this.chk_Date.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Date.Size = new System.Drawing.Size(176, 24);
            this.chk_Date.TabIndex = 5;
            this.chk_Date.Text = "Date";
            this.chk_Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Date.UseVisualStyleBackColor = true;
            this.chk_Date.Visible = false;
            // 
            // chk_SubLedger
            // 
            this.chk_SubLedger.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.chk_SubLedger.Location = new System.Drawing.Point(338, 84);
            this.chk_SubLedger.Name = "chk_SubLedger";
            this.chk_SubLedger.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_SubLedger.Size = new System.Drawing.Size(176, 24);
            this.chk_SubLedger.TabIndex = 7;
            this.chk_SubLedger.Text = "Sub Ledger";
            this.chk_SubLedger.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_SubLedger.UseVisualStyleBackColor = true;
            // 
            // cmb_GroupBy
            // 
            this.cmb_GroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_GroupBy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_GroupBy.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.cmb_GroupBy.FormattingEnabled = true;
            this.cmb_GroupBy.Items.AddRange(new object[] {
            "----Select---",
            "Agent",
            "Area",
            "Account Group",
            "Account Sub Group"});
            this.cmb_GroupBy.Location = new System.Drawing.Point(130, 114);
            this.cmb_GroupBy.Name = "cmb_GroupBy";
            this.cmb_GroupBy.Size = new System.Drawing.Size(188, 28);
            this.cmb_GroupBy.TabIndex = 3;
            // 
            // lbl_GroupBy
            // 
            this.lbl_GroupBy.AutoSize = true;
            this.lbl_GroupBy.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_GroupBy.Location = new System.Drawing.Point(11, 117);
            this.lbl_GroupBy.Name = "lbl_GroupBy";
            this.lbl_GroupBy.Size = new System.Drawing.Size(85, 20);
            this.lbl_GroupBy.TabIndex = 64;
            this.lbl_GroupBy.Text = "Group By";
            // 
            // chk_AllLedger
            // 
            this.chk_AllLedger.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.chk_AllLedger.Location = new System.Drawing.Point(338, 110);
            this.chk_AllLedger.Name = "chk_AllLedger";
            this.chk_AllLedger.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_AllLedger.Size = new System.Drawing.Size(176, 24);
            this.chk_AllLedger.TabIndex = 8;
            this.chk_AllLedger.Text = "All Ledger";
            this.chk_AllLedger.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_AllLedger.UseVisualStyleBackColor = true;
            // 
            // msk_ToDate
            // 
            this.msk_ToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_ToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.msk_ToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.msk_ToDate.Location = new System.Drawing.Point(130, 86);
            this.msk_ToDate.Mask = "00/0000";
            this.msk_ToDate.Name = "msk_ToDate";
            this.msk_ToDate.Size = new System.Drawing.Size(115, 26);
            this.msk_ToDate.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label2.Location = new System.Drawing.Point(11, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 20);
            this.label2.TabIndex = 57;
            this.label2.Text = "To Month";
            // 
            // msk_FromDate
            // 
            this.msk_FromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_FromDate.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.msk_FromDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.msk_FromDate.Location = new System.Drawing.Point(130, 59);
            this.msk_FromDate.Mask = "00/0000";
            this.msk_FromDate.Name = "msk_FromDate";
            this.msk_FromDate.Size = new System.Drawing.Size(115, 26);
            this.msk_FromDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label1.Location = new System.Drawing.Point(11, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 20);
            this.label1.TabIndex = 55;
            this.label1.Text = "From Month";
            // 
            // btn_Show
            // 
            this.btn_Show.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 16F);
            this.btn_Show.Appearance.Options.UseFont = true;
            this.btn_Show.Location = new System.Drawing.Point(355, 177);
            this.btn_Show.Name = "btn_Show";
            this.btn_Show.Size = new System.Drawing.Size(77, 35);
            this.btn_Show.TabIndex = 9;
            this.btn_Show.Text = "&Show";
            this.btn_Show.Click += new System.EventHandler(this.btn_Show_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 16F);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(438, 177);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 35);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmMonthlyPartyLedger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(527, 212);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btn_Show);
            this.Controls.Add(this.gb_TBOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmMonthlyPartyLedger";
            this.Text = "Monthly Party Ledger";
            this.Load += new System.EventHandler(this.MonthlyPartyLedger_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MonthlyPartyLedger_KeyPress);
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
        private System.Windows.Forms.CheckBox chk_SubLedger;
        private System.Windows.Forms.ComboBox cmb_GroupBy;
        private System.Windows.Forms.Label lbl_GroupBy;
        private System.Windows.Forms.CheckBox chk_AllLedger;
        private System.Windows.Forms.MaskedTextBox msk_ToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox msk_FromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chk_IncludeZeroBal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSysDateType;
        private DevExpress.XtraEditors.SimpleButton btn_Show;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}