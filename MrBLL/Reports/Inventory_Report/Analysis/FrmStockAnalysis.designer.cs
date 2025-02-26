using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Inventory_Report.Analysis
{
    partial class FrmStockAnalysis
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
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSysDateType = new System.Windows.Forms.ComboBox();
            this.lbl_Branch = new System.Windows.Forms.Label();
            this.cmb_Branch = new System.Windows.Forms.ComboBox();
            this.chk_ZeroBalance = new System.Windows.Forms.CheckBox();
            this.chk_IncludeFree = new System.Windows.Forms.CheckBox();
            this.chk_ExtraFree = new System.Windows.Forms.CheckBox();
            this.chk_SelectAll = new System.Windows.Forms.CheckBox();
            this.chk_ExcludeUnBilledChallan = new System.Windows.Forms.CheckBox();
            this.cmb_GroupBy = new System.Windows.Forms.ComboBox();
            this.lbl_GroupBy = new System.Windows.Forms.Label();
            this.chk_SalesBuyRate = new System.Windows.Forms.CheckBox();
            this.msk_ToDate = new MrMaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.msk_FromDate = new MrMaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gb_TBOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Show
            // 
            this.btn_Show.BackColor = System.Drawing.Color.SteelBlue;
            this.btn_Show.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Show.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Show.ForeColor = System.Drawing.SystemColors.Window;
            this.btn_Show.Location = new System.Drawing.Point(326, 166);
            this.btn_Show.Name = "btn_Show";
            this.btn_Show.Size = new System.Drawing.Size(81, 30);
            this.btn_Show.TabIndex = 15;
            this.btn_Show.Text = "&Show";
            this.btn_Show.UseVisualStyleBackColor = false;
            this.btn_Show.Click += new System.EventHandler(this.btn_Show_Click);
            // 
            // gb_TBOptions
            // 
            this.gb_TBOptions.Controls.Add(this.label4);
            this.gb_TBOptions.Controls.Add(this.cmbSysDateType);
            this.gb_TBOptions.Controls.Add(this.lbl_Branch);
            this.gb_TBOptions.Controls.Add(this.cmb_Branch);
            this.gb_TBOptions.Controls.Add(this.chk_ZeroBalance);
            this.gb_TBOptions.Controls.Add(this.chk_IncludeFree);
            this.gb_TBOptions.Controls.Add(this.chk_ExtraFree);
            this.gb_TBOptions.Controls.Add(this.chk_SelectAll);
            this.gb_TBOptions.Controls.Add(this.chk_ExcludeUnBilledChallan);
            this.gb_TBOptions.Controls.Add(this.cmb_GroupBy);
            this.gb_TBOptions.Controls.Add(this.lbl_GroupBy);
            this.gb_TBOptions.Controls.Add(this.chk_SalesBuyRate);
            this.gb_TBOptions.Controls.Add(this.msk_ToDate);
            this.gb_TBOptions.Controls.Add(this.label2);
            this.gb_TBOptions.Controls.Add(this.msk_FromDate);
            this.gb_TBOptions.Controls.Add(this.label1);
            this.gb_TBOptions.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_TBOptions.Location = new System.Drawing.Point(3, -7);
            this.gb_TBOptions.Name = "gb_TBOptions";
            this.gb_TBOptions.Size = new System.Drawing.Size(496, 170);
            this.gb_TBOptions.TabIndex = 14;
            this.gb_TBOptions.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 11F);
            this.label4.Location = new System.Drawing.Point(9, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 17);
            this.label4.TabIndex = 91;
            this.label4.Text = "Date Type";
            // 
            // cmbSysDateType
            // 
            this.cmbSysDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSysDateType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbSysDateType.Font = new System.Drawing.Font("Arial", 11F);
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
            this.cmbSysDateType.Location = new System.Drawing.Point(128, 15);
            this.cmbSysDateType.Name = "cmbSysDateType";
            this.cmbSysDateType.Size = new System.Drawing.Size(191, 25);
            this.cmbSysDateType.TabIndex = 0;
            this.cmbSysDateType.SelectedIndexChanged += new System.EventHandler(this.cmbSysDateType_SelectedIndexChanged);
            this.cmbSysDateType.Enter += new System.EventHandler(this.cmbSysDateType_Enter);
            this.cmbSysDateType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSysDateType_KeyDown);
            this.cmbSysDateType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbSysDateType_KeyPress);
            this.cmbSysDateType.Leave += new System.EventHandler(this.cmbSysDateType_Leave);
            // 
            // lbl_Branch
            // 
            this.lbl_Branch.AutoSize = true;
            this.lbl_Branch.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Branch.Location = new System.Drawing.Point(9, 132);
            this.lbl_Branch.Name = "lbl_Branch";
            this.lbl_Branch.Size = new System.Drawing.Size(72, 20);
            this.lbl_Branch.TabIndex = 79;
            this.lbl_Branch.Text = "Branch ";
            // 
            // cmb_Branch
            // 
            this.cmb_Branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Branch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_Branch.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Branch.FormattingEnabled = true;
            this.cmb_Branch.Location = new System.Drawing.Point(127, 129);
            this.cmb_Branch.Name = "cmb_Branch";
            this.cmb_Branch.Size = new System.Drawing.Size(189, 28);
            this.cmb_Branch.TabIndex = 5;
            // 
            // chk_ZeroBalance
            // 
            this.chk_ZeroBalance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_ZeroBalance.Location = new System.Drawing.Point(320, 63);
            this.chk_ZeroBalance.Name = "chk_ZeroBalance";
            this.chk_ZeroBalance.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_ZeroBalance.Size = new System.Drawing.Size(169, 26);
            this.chk_ZeroBalance.TabIndex = 8;
            this.chk_ZeroBalance.Text = "Zero Balance";
            this.chk_ZeroBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_ZeroBalance.UseVisualStyleBackColor = true;
            // 
            // chk_IncludeFree
            // 
            this.chk_IncludeFree.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_IncludeFree.Location = new System.Drawing.Point(320, 88);
            this.chk_IncludeFree.Name = "chk_IncludeFree";
            this.chk_IncludeFree.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_IncludeFree.Size = new System.Drawing.Size(169, 26);
            this.chk_IncludeFree.TabIndex = 9;
            this.chk_IncludeFree.Text = "Include Free";
            this.chk_IncludeFree.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_IncludeFree.UseVisualStyleBackColor = true;
            // 
            // chk_ExtraFree
            // 
            this.chk_ExtraFree.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_ExtraFree.Location = new System.Drawing.Point(320, 111);
            this.chk_ExtraFree.Name = "chk_ExtraFree";
            this.chk_ExtraFree.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_ExtraFree.Size = new System.Drawing.Size(169, 26);
            this.chk_ExtraFree.TabIndex = 10;
            this.chk_ExtraFree.Text = "Extra Free";
            this.chk_ExtraFree.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_ExtraFree.UseVisualStyleBackColor = true;
            // 
            // chk_SelectAll
            // 
            this.chk_SelectAll.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_SelectAll.Location = new System.Drawing.Point(320, 136);
            this.chk_SelectAll.Name = "chk_SelectAll";
            this.chk_SelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_SelectAll.Size = new System.Drawing.Size(169, 26);
            this.chk_SelectAll.TabIndex = 11;
            this.chk_SelectAll.Text = "Select All";
            this.chk_SelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_SelectAll.UseVisualStyleBackColor = true;
            // 
            // chk_ExcludeUnBilledChallan
            // 
            this.chk_ExcludeUnBilledChallan.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_ExcludeUnBilledChallan.Location = new System.Drawing.Point(320, 40);
            this.chk_ExcludeUnBilledChallan.Name = "chk_ExcludeUnBilledChallan";
            this.chk_ExcludeUnBilledChallan.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_ExcludeUnBilledChallan.Size = new System.Drawing.Size(169, 26);
            this.chk_ExcludeUnBilledChallan.TabIndex = 7;
            this.chk_ExcludeUnBilledChallan.Text = "Exclude UnBilled Challan";
            this.chk_ExcludeUnBilledChallan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_ExcludeUnBilledChallan.UseVisualStyleBackColor = true;
            // 
            // cmb_GroupBy
            // 
            this.cmb_GroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_GroupBy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_GroupBy.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_GroupBy.FormattingEnabled = true;
            this.cmb_GroupBy.Items.AddRange(new object[] {
            "Product",
            "Product Group",
            "Product Sub Group"});
            this.cmb_GroupBy.Location = new System.Drawing.Point(128, 98);
            this.cmb_GroupBy.Name = "cmb_GroupBy";
            this.cmb_GroupBy.Size = new System.Drawing.Size(189, 28);
            this.cmb_GroupBy.TabIndex = 4;
            // 
            // lbl_GroupBy
            // 
            this.lbl_GroupBy.AutoSize = true;
            this.lbl_GroupBy.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_GroupBy.Location = new System.Drawing.Point(9, 104);
            this.lbl_GroupBy.Name = "lbl_GroupBy";
            this.lbl_GroupBy.Size = new System.Drawing.Size(85, 20);
            this.lbl_GroupBy.TabIndex = 64;
            this.lbl_GroupBy.Text = "Group By";
            // 
            // chk_SalesBuyRate
            // 
            this.chk_SalesBuyRate.Checked = true;
            this.chk_SalesBuyRate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_SalesBuyRate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_SalesBuyRate.Location = new System.Drawing.Point(320, 14);
            this.chk_SalesBuyRate.Name = "chk_SalesBuyRate";
            this.chk_SalesBuyRate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_SalesBuyRate.Size = new System.Drawing.Size(169, 26);
            this.chk_SalesBuyRate.TabIndex = 6;
            this.chk_SalesBuyRate.Text = "Sales/Buy Rate";
            this.chk_SalesBuyRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_SalesBuyRate.UseVisualStyleBackColor = true;
            // 
            // msk_ToDate
            // 
            this.msk_ToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_ToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msk_ToDate.Location = new System.Drawing.Point(128, 70);
            this.msk_ToDate.Mask = "00/00/0000";
            this.msk_ToDate.Name = "msk_ToDate";
            this.msk_ToDate.Size = new System.Drawing.Size(140, 26);
            this.msk_ToDate.TabIndex = 3;
            this.msk_ToDate.Enter += new System.EventHandler(this.msk_ToDate_Enter);
            this.msk_ToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msk_ToDate_KeyDown);
            this.msk_ToDate.Leave += new System.EventHandler(this.msk_ToDate_Leave);
            this.msk_ToDate.Validated += new System.EventHandler(this.msk_ToDate_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 57;
            this.label2.Text = "To Date";
            // 
            // msk_FromDate
            // 
            this.msk_FromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_FromDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msk_FromDate.Location = new System.Drawing.Point(128, 43);
            this.msk_FromDate.Mask = "00/00/0000";
            this.msk_FromDate.Name = "msk_FromDate";
            this.msk_FromDate.Size = new System.Drawing.Size(140, 26);
            this.msk_FromDate.TabIndex = 2;
            this.msk_FromDate.Enter += new System.EventHandler(this.msk_FromDate_Enter);
            this.msk_FromDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msk_FromDate_KeyDown);
            this.msk_FromDate.Leave += new System.EventHandler(this.msk_FromDate_Leave);
            this.msk_FromDate.Validated += new System.EventHandler(this.msk_FromDate_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 20);
            this.label1.TabIndex = 55;
            this.label1.Text = "From Date";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.SteelBlue;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.Window;
            this.btnCancel.Location = new System.Drawing.Point(413, 167);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 30);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmStockAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(499, 199);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btn_Show);
            this.Controls.Add(this.gb_TBOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmStockAnalysis";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Analysis";
            this.Load += new System.EventHandler(this.FrmStockAnalysis_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmStockAnalysis_KeyPress);
            this.gb_TBOptions.ResumeLayout(false);
            this.gb_TBOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Show;
        private System.Windows.Forms.GroupBox gb_TBOptions;
        private System.Windows.Forms.CheckBox chk_ZeroBalance;
        private System.Windows.Forms.CheckBox chk_IncludeFree;
        private System.Windows.Forms.CheckBox chk_ExtraFree;
        private System.Windows.Forms.CheckBox chk_SelectAll;
        private System.Windows.Forms.CheckBox chk_ExcludeUnBilledChallan;
        private System.Windows.Forms.ComboBox cmb_GroupBy;
        private System.Windows.Forms.Label lbl_GroupBy;
        private System.Windows.Forms.CheckBox chk_SalesBuyRate;
        private System.Windows.Forms.MaskedTextBox msk_ToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox msk_FromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Branch;
        private System.Windows.Forms.ComboBox cmb_Branch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSysDateType;
        private System.Windows.Forms.Button btnCancel;
    }
}